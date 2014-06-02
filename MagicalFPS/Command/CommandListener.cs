using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MagicalFPS.Input;
using MMF.Utility;
using OculusForMikuMikuFlex;
using SlimDX;

namespace MagicalFPS.Command
{
    internal class CommandListener : IDisposable
    {
        #region コマンド処理部分

        private readonly GameContext _context;

        private bool isCanceled;

        /// コマンド名,Tuple
        /// <メソッド名, コマンド属性>
        private readonly Dictionary<string, Tuple<string, CommandAttribute>> commands =
            new Dictionary<string, Tuple<string, CommandAttribute>>();

        public CommandListener(GameContext context)
        {
            _context = context;
            //このクラスの中のCommandAttributeがついているメソッドを列挙
            foreach (MethodInfo methodInfo in GetType().GetMethods())
            {
                var attr = methodInfo.GetCustomAttribute<CommandAttribute>();
                if (attr != null)
                {
                    ParameterInfo[] paramsInfo = methodInfo.GetParameters();
                    if (paramsInfo.Length != 1)
                    {
                        Tracer.w("コマンド{0}は引数の数が正しくないため読み込みはスキップされました。", attr.CommandName);
                        continue;
                    }
                    if (!paramsInfo[0].ParameterType.IsEquivalentTo(typeof (string[])))
                    {
                        Tracer.w("コマンド{0}は引数の型が正しくないため読み込みはスキップされました。", attr.CommandName);
                        continue;
                    }
                    commands.Add(attr.CommandName, new Tuple<string, CommandAttribute>(methodInfo.Name, attr));
                }
            }
            Task.Run((Action) Listen);
        }

        private void Listen()
        {
            while (!isCanceled)
            {
                string command = Console.ReadLine();
                string[] splittedCommand = command.Split(' ');
                if (commands.ContainsKey(splittedCommand[0]))
                {
                    Tuple<string, CommandAttribute> commandInfo = commands[splittedCommand[0]];
                    MethodInfo m = GetType().GetMethod(commandInfo.Item1);
                    var commandArgs = new string[splittedCommand.Length - 1];
                    for (int i = 1; i < splittedCommand.Length; i++)
                    {
                        commandArgs[i - 1] = splittedCommand[i];
                    }
                    var args = new object[1] {commandArgs};
                    if (commandInfo.Item2.ArgCount != -1 && commandInfo.Item2.ArgCount != commandArgs.Length)
                    {
                        Tracer.w("コマンド{0}の引数の数が不正です。{1}ではなく{2}個です。", commandInfo.Item2.CommandName, args.Length,
                            commandInfo.Item2.ArgCount);
                        continue;
                    }
                    m.Invoke(this, args);
                }
                else
                {
                    Tracer.w("コマンド{0}は登録されていません。", splittedCommand[0]);
                }
            }
        }

        public void Dispose()
        {
            isCanceled = true;
        }

        #endregion

        [Command("di-enum-devices",0,"現在接続されている入力デバイスを列挙します")]
        public void DirectInputEnumDevices(string[] args)
        {
            Tracer.i(_context.DirectInput.deviceTraces);
        }

        [Command("di-reload-devices", 0,"現在接続されている入力デバイスを更新します")]
        public void DirectInputReloadDevices(string[] args)
        {
            _context.DirectInput.UpdateDevices();
        }

        [Command("enum-cmd", 0,"現在登録されているコマンドを列挙します")]
        public void EnumCommands(string[] args)
        {
            StringBuilder builder=new StringBuilder();
            builder.AppendFormat("登録されたコマンド数:{0}\n", commands.Count);
            foreach (var command in commands)
            {
                builder.AppendFormat("   コマンド:{0}  -  {1}\n", command.Key, command.Value.Item2.Description);
            }
            Tracer.i(builder.ToString());
        }

        [Command("hi-set-keyboard",2,"入力デバイスとしてキーボードをセットします。")]
        public void SetKeyboardHendOperationDevice(string[] args)
        {
            _context.PlayerContexts[int.Parse(args[0])].HandOperationChecker=new KeyboardHandOperationChecker(_context.DirectInput,int.Parse(args[1]));
            Tracer.i("{0}のキーボードデバイスを入力デバイスとして選択しました。",int.Parse(args[0]));
        }

        [Command("hi-set-joystick", 2, "入力デバイスとしてジョイスティックをセットします。")]
        public void SetJoyStickHendOperationDevice(string[] args)
        {
            _context.PlayerContexts[int.Parse(args[0])].HandOperationChecker = new JoystickHandOperationChecker(_context, int.Parse(args[1]));
            Tracer.i("{0}のジョイスティックを入力デバイスとして選択しました。", int.Parse(args[1]));
        }

        [Command("toggle-grid",0,"グリッドの表示状態を切り替えます。")]
        public void ToggleDebugGrid(string[] args)
        {
            _context.DebugGrid.Visibility = !_context.DebugGrid.Visibility;
            Tracer.i("デバッググリッドの表示状態:{0}",_context.DebugGrid.Visibility);
        }

        [Command("set-v-eyeDist",1,"目の間隔の距離を調整します。")]
        public void SetEyeDistance(string[] args)
        {
            foreach (var playerContext in _context.PlayerContexts)
            {
                playerContext.EyeTextureRenderer.EyeDistance = float.Parse(args[0]);
            }
        }

        [Command("set-v-lensOffset", 2, "レンズのオフセットを調整します")]
        public void SetLensCenterOffset(string[] args)
        {
            OculusDisplayRenderer.RiftLensCenterOffset=new Vector2(float.Parse(args[0]),float.Parse(args[1]));
            Tracer.i("レンズオフセット:{0}",OculusDisplayRenderer.RiftLensCenterOffset);
        }

        [Command("start-effect", 0, "エフェクトの再生をテストします。")]
        public void StartEffect(string[] args)
        {
            _context.StartEffect();
        }

        [Command("init-players", 0, "プレイヤー画面を初期化します。")]
        public void InitializePlayers(string[] args)
        {
            _context.InitializePlayerContexts();
        }
    }
}
