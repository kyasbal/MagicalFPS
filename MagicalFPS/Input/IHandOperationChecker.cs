using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimDX;

namespace MagicalFPS.Input
{
    public interface IHandOperationChecker
    {
        /// <summary>
        /// 移動に用いる入力ベクトル
        /// </summary>
        /// <returns>1000～-1000で表された傾きの量</returns>
        Vector2 getMovementVector();

        /// <summary>
        /// OKボタンが押されているかどうか
        /// </summary>
        /// <returns></returns>
        bool isAcceptButtonPressed();

        /// <summary>
        /// Cancelボタンが押されているかどうか
        /// </summary>
        /// <returns></returns>
        bool isCancelButtonPressed();

        /// <summary>
        /// ジャンプボタンが押されているかどうか
        /// </summary>
        /// <returns></returns>
        bool isJumpButtonPressed();

        /// <summary>
        /// しゃがむボタンが押されてるかどうか
        /// </summary>
        /// <returns></returns>
        bool isSitButtonPressed();
    }
}
