## Study of .NET Community Toolkit

Created in 2022/08

### Environment

.NET6 + C#10 + WPF

CommunityToolkit.Mvvm  8.0.0

### Memo

名前空間（フォルダ構成）は [Introduction to the MVVM Toolkit](https://docs.microsoft.com/ja-jp/windows/communitytoolkit/mvvm/introduction) のリストを参考にしました。

### Links

[Announcing .NET Community Toolkit 8.0! MVVM, Diagnostics, Performance, and more! - .NET Blog](https://devblogs.microsoft.com/dotnet/announcing-the-dotnet-community-toolkit-800/)

[CommunityToolkit - GitHub](https://github.com/CommunityToolkit/dotnet)

[Community Toolkits のドキュメント | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/)



## 以降、雑多メモ

### Packages

- [`CommunityToolkit.Mvvm` (MVVM Toolkitとも呼ばれます)](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/): 高速でモジュール化されたプラットフォームに依存しない MVVM ライブラリ。[`MvvmLight`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/migratingfrommvvmlight)これは、 これは、Microsoft Storeやその他のファースト パーティ アプリで広く使用されています。
- `CommunityToolkit.Mvvm.SourceGenerators`: MVVM Toolkitを拡張するソース ジェネレーター。
- [`CommunityToolkit.Diagnostics`](https://docs.microsoft.com/ja-JP/windows/communitytoolkit/diagnostics/introduction): よりクリーンで効率的で、エラーが発生しやすい引数の検証とエラー チェックに使用できるヘルパー API (具体的には、Guard と ThrowHelper) のセット。
- [`CommunityToolkit.HighPerformance`](https://docs.microsoft.com/ja-JP/windows/communitytoolkit/high-performance/introduction) は、高パフォーマンスのシナリオで作業するためのヘルパーのコレクションです。 これには、プールされたバッファー ヘルパー、高速文字列プール型、2D バリアントの `Memory<T>` and `Span<T>` ([`Memory2D`](https://docs.microsoft.com/ja-JP/windows/communitytoolkit/high-performance/memory2d)および[`Span2D`](https://docs.microsoft.com/ja-JP/windows/communitytoolkit/high-performance/span2d)) などの API が含まれています。また、不連続領域、ビット シフト操作のヘルパー (`BitHelper`[ペイント.NET](https://www.getpaint.net/) でも使用される) などがサポートされています。
- `CommunityToolkit.Common`: 他の CommunityToolkit ライブラリと共有されるヘルパー API のセット。

### Types

- CommunityToolkit.Mvvm.ComponentModel
  - [`ObservableObject`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/observableobject)
  - [`ObservableRecipient`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/observablerecipient)
  - [`ObservableValidator`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/observablevalidator)
- CommunityToolkit.Mvvm.DependencyInjection
  - [`Ioc`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/ioc)
- CommunityToolkit.Mvvm.Input
  - [`RelayCommand`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/relaycommand)
  - [`RelayCommand`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/relaycommand)
  - [`AsyncRelayCommand`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/asyncrelaycommand)
  - [`AsyncRelayCommand`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/asyncrelaycommand)
  - [`IRelayCommand`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/relaycommand)
  - [`IRelayCommand`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/relaycommand)
  - [`IAsyncRelayCommand`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/asyncrelaycommand)
  - [`IAsyncRelayCommand`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/asyncrelaycommand)
- CommunityToolkit.Mvvm.Messaging
  - [`IMessenger`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
  - [`WeakReferenceMessenger`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
  - [`StrongReferenceMessenger`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
  - [`IRecipient`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
  - [`MessageHandler`](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/messenger)
- CommunityToolkit.Mvvm.Messaging.Messages
  - [`PropertyChangedMessage`](https://docs.microsoft.com/ja-jp/dotnet/api/CommunityToolkit.mvvm.Messaging.Messages.PropertyChangedMessage-1)
  - [`RequestMessage`](https://docs.microsoft.com/ja-jp/dotnet/api/CommunityToolkit.mvvm.Messaging.Messages.RequestMessage-1)
  - [`AsyncRequestMessage`](https://docs.microsoft.com/ja-jp/dotnet/api/CommunityToolkit.mvvm.Messaging.Messages.AsyncRequestMessage-1)
  - [`CollectionRequestMessage`](https://docs.microsoft.com/ja-jp/dotnet/api/CommunityToolkit.mvvm.Messaging.Messages.CollectionRequestMessage-1)
  - [`AsyncCollectionRequestMessage`](https://docs.microsoft.com/ja-jp/dotnet/api/CommunityToolkit.mvvm.Messaging.Messages.AsyncCollectionRequestMessage-1)
  - [`ValueChangedMessage`](https://docs.microsoft.com/ja-jp/dotnet/api/CommunityToolkit.mvvm.Messaging.Messages.ValueChangedMessage-1)



### 理解できてない

[FlowExceptionsToTaskScheduler](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/relaycommand#handling-asynchronous-exceptions)



### ポイント

INotifyPropertyChanged ソースジェネは使わなくて済むなら使うなってよ。[INotifyPropertyChanged 属性 - .NET Community Toolkit | Microsoft Docs](https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/inotifypropertychanged)

