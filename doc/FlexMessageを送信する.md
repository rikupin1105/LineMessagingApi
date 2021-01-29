# 「Hello,World!」メッセージを送る
最初のステップとして、「Hello, World!」メッセージを作成してみましょう。  
![HelloWorld!](img/Flex_1.png)
```cs
new FlexMessage("代替テキスト")
{
    Contents = new BubbleContainer() //1
    {
        Body = new BoxComponent() //2
        {
            Layout = BoxLayout.Horizontal, //3
            Contents = new IFlexComponent[] //4
            {
                new TextComponent()
                {
                    Text="hello" //5
                },
                new TextComponent()
                {
                    Text="world"//5
                }
            }
        }
    }
}
```
1-5の意味は以下のとおりです。
|    |    |
| ---- | ---- |
|  1  |  単体のメッセージバブルを表すバブルを作成します。  |
|  2  |  ボックスを作成します。  |
|  3  |  ボックスの layout プロパティに horizontal を指定します。  |
|  4  |  ボックスの contents プロパティに、ボックスに含めるコンポーネントを配列で指定します。  |
|  5  |  「Hello,」と「World!」の2つのテキストを挿入します。  |

<br>
<br>

## 送信する
```cs
var messages = new ISendMessage[]
{
    new FlexMessage("代替テキスト")
    {
        Contents = new BubbleContainer()
        {
            Body = new BoxComponent()
            {
                Layout=BoxLayout.Horizontal,
                Contents = new IFlexComponent[]
                {
                    new TextComponent()
                    {
                        Text="hello"
                    },
                    new TextComponent()
                    {
                        Text="world"
                    }
                }
            }
        }
    }
};
await LineMessagingClient.ReplyMessageAsync("リプライトークン", messages);
```