操作には、Oculus、Kinect、gamepadを用いる予定です。  
プレイヤーには左手でgamepadを持ってもらい、右手をKinectで認識させる予定です。（Oculusは顔です）  
## 1. 移動方法
* 座標移動  
前後左右> gamepadの3Dスティック(or十字キー)  
上下> gamepadの十字キー上下
* 軸回転移動  
左右回転> oculusに初期位置を設定し、初期位置からのoculusの左右のズレを体の回転に反映。ズレの大きさにより回転かチラ見かを判断。  
上下回転> 今のところ、実装予定なし。  

## 2. 魔法
1キャラにつき3種の魔法が使えるようにする予定です。  
* 発動  
右手の特定の動作をKinectで認識することで、魔法を発動させる予定です。

* トリガー一覧  
A　右手を振り下ろす  
![A](https://raw.githubusercontent.com/LimeStreem/MagicalFPS/master/wiki/A.gif)  
B　右手を横に振る  
![B](https://raw.githubusercontent.com/LimeStreem/MagicalFPS/master/wiki/B.gif)  
C　右手で円を描く  
![C](https://raw.githubusercontent.com/LimeStreem/MagicalFPS/master/wiki/C.gif)  
D　右手を床まで振り下ろす  
![D](https://raw.githubusercontent.com/LimeStreem/MagicalFPS/master/wiki/D.gif)  

[注]なお、魔法の詳細はキャラクターのページに記載します。

