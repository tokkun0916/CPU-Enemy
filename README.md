# CPU-Enemy

## 概要
高度なCPU設計を最終目標に様々な要素を汎用化できるように作成するプロジェクト

## 使用技術
- Unity 6000.3.11f1
- C#
- DOTween
- UniTask
- UniRx

## 担当
個人制作

# 実装した機能一覧
- ダメージエリア
- ObjectPoolのGC検証

## ダメージエリアの工夫した点
- Spawner/Factory/Runnerのライフサイクル
- Runnerの状態をUniTaskで進め、UniRxで状態を購読し各機能へ通知するロジック
- DoTween、UniTask以外で発生するGC.Allocをなくした

## ObjectPoolのGC検証結果

### GC.Alloc
<p>
  <img src="images\compare-gcalloc.png" width="400">
  <img src="images\compare-gcalloc-600k-ver.png" width="400">
</p>

ObjectPoolを使用した時の方がInstantiate,Destoryよりも
GC.Allocを約35~54%削減することができた

### AvarageFPS
<p>
  <img src="images\compare-avarage-fps.png" width="400">
  <img src="images\compare-avarage-fps-600k-ver.png" width="400">
</p>

ObjectPoolは初めにPoolを生成する時間が発生してしまうが
平均FPSを見るとオブジェクトを複数回使う時にObjectPoolの方が優秀だと言える

### ObjectPoolのGC検証結果まとめ
オブジェクトを頻繫に生成する場合やゲーム実行中のGCによるラグを避けるなら
ObjectPoolがパフォーマンス最適化の有効な手段だと言える

## 制作背景と目標
得意な事を伸ばそうと考え思いついたのがCPU
高度な判断基準を素早く処理できるCPU実装とCPUに関する機能を可読性良く実装する
