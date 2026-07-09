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
・Spawner/Factory/Runnerのライフサイクル
・Runnerの状態をUniTaskで進め、UniRxで状態を購読し各機能へ通知するロジック
・GC削減

## ObjectPoolのGC検証結果

<p>
  <img src="images/compare-gcalloc.png" width="400">
  <img src="images/compare-gcalloc-60m-ver.png" width="400">
</p>

## 制作背景と目標
得意な事を伸ばそうと考え思いついたのがCPU
高度な判断基準を素早く処理できるCPU実装とCPUに関する機能を可読性良く実装する
