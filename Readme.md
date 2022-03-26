# Inspector Component Filter
## 導入
- 当ページから**Releases**へ移動してください
![スクリーンショット 2022-03-26 193820](https://user-images.githubusercontent.com/42258351/160235901-9dc4b162-081e-4b25-a4e4-af210fa67707.png)

- Assets欄から**InspectorComponentFilter_ver.x.x.x.unitypackage**をクリックし、ダウンロードしてください
![スクリーンショット 2022-03-26 193820](https://user-images.githubusercontent.com/42258351/160235975-5f5d2b98-9676-4204-9826-687d18c5c41f.png)

- ダウンロード後、任意のプロジェクトのprojectウィンドウにunitypakcageをドラッグし、インポートしてください

## 使い方
- 上部メニューのTool > InspectorComponentFilterをクリックすると検索ウィンドウが開きます。
![スクリーンショット 2022-03-26 193820](https://user-images.githubusercontent.com/42258351/160236151-e3fb6102-3815-4e44-8841-0e6847dac2d7.png)
  - 下記画像のようにウィンドウをドラックし、inspector上部に配置も可能です。
  ![スクリーンショット 2022-03-26 193820](https://user-images.githubusercontent.com/42258351/160236492-34c9db82-d348-4e84-99e1-718f5918ff8c.png)

- hierarchyでオブジェクトを選択した状態で、検索ウィンドウの入力欄にコンポーネント名を入力してください
- 検索文字列と前方一致したコンポーネントがinspectorに表示されます
![スクリーンショット 2022-03-26 193820](https://user-images.githubusercontent.com/42258351/160236274-c50a7c03-7950-444b-a399-f2dc4b3e2e3e.png)

### フィルターのリセット
- ウィンドウを閉じる・入力欄を空にする・入力欄横のリセットボタン押下で検索結果をリセットし、inspectorの表示をもとに戻すことができます。

![スクリーンショット 2022-03-26 193820](https://user-images.githubusercontent.com/42258351/160236462-871515c2-ebb7-457a-87b1-6f42bae2316b.png)

## 注意点
- オブジェクトに取り付けられているコンポーネントの数が多いと、検索時の動作が重くなる可能性があります。
- hierarchy上でオブジェクトを複数選択した場合、正しい検索結果が出ない場合があります。
  - コピーしたオブジェクトなど同一のコンポーネントがあるオブジェクトの場合は検索可能です。
