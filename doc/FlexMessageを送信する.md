# 「Hello,World!」メッセージを送る
最初のステップとして、「Hello, World!」メッセージを作成してみましょう。  
![HelloWorld!](https://github.com/rikupin1105/LineMessagingApi/blob/main/doc/IMG/Flex_1.png?raw=true)
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
1. 単体のメッセージバブルを表すバブルを作成します。
2. ボックスを作成します。
3. ボックスの layout プロパティに horizontal を指定します。
4. ボックスの contents プロパティに、ボックスに含めるコンポーネントを配列で指定します。
5. 「Hello」と「World!」の2つのテキストを挿入します。

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
                Layout = BoxLayout.Horizontal,
                Contents = new IFlexComponent[]
                {
                    new TextComponent()
                    {
                        Text = "hello"
                    },
                    new TextComponent()
                    {
                        Text = "world"
                    }
                }
            }
        }
    }
};
//リプライやプッシュで送信する
await LineMessagingClient.ReplyMessageAsync("リプライトークン", messages);
await LineMessagingClient.PushMessageAsync("to",messages);
```
<br>

## サンプル 1
---
![Flex1](https://github.com/rikupin1105/LineMessagingApi/blob/main/doc/IMG/Flex_2.jpg?raw=true)
```cs
new FlexMessage("フレックスメッセージ1")
{
    Contents = new BubbleContainer()
    {
        Size = BubbleSize.mega,
        Header = new BoxComponent()
        {
            Layout = BoxLayout.Vertical,
            Contents = new IFlexComponent[]
            {
                new BoxComponent()
                {
                    Layout = BoxLayout.Vertical,
                    Contents = new IFlexComponent[]
                    {
                       new TextComponent()
                       {
                           Text="FROM",
                           Color="#ffffff66",
                           Size="sm"
                       },
                       new TextComponent()
                       {
                           Text="Akihabara",
                           Color="#ffffff",
                           Size= "xl",
                           Flex=4,
                           Weight=Weight.Bold
                       }
                    }
                },
                new BoxComponent()
                {
                    Layout=BoxLayout.Vertical,
                    Contents = new IFlexComponent[]
                    {
                        new TextComponent()
                        {
                            Text="TO",
                            Color="#ffffff66",
                            Size="sm"
                        },
                        new TextComponent()
                        {
                            Text="Shinjuku",
                            Color="#ffffff",
                            Size="xl",
                            Flex=4,
                            Weight= Weight.Bold
                        }
                    }
                }
            },
            PaddingAll="20px",
            BackgroundColor="#0367D3",
            Spacing="md",
            Height="154px",
            PaddingTop="22px"
        },
        Body = new BoxComponent()
        {
            Layout=BoxLayout.Vertical,
            Contents = new IFlexComponent[]
            {
                new TextComponent()
                {
                    Text="Total: 1 hour",
                    Color = "#b7b7b7",
                    Size = "xs"
                },
                new BoxComponent()
                {
                    Layout=BoxLayout.Horizontal,
                    Contents = new IFlexComponent[]
                    {
                        new BoxComponent()
                        {
                            Layout = BoxLayout.Horizontal,
                            Contents = new IFlexComponent[]
                            {
                                new TextComponent()
                                {
                                    Text = "20:30",
                                    Size = "sm",
                                    Gravity = Gravity.Center
                                },
                            },
                            Flex=1
                        },
                        new BoxComponent()
                        {
                            Layout = BoxLayout.Vertical,
                            Contents = new IFlexComponent[]
                            {
                                new FillerComponent()
                                {
                                },
                                new BoxComponent()
                                {
                                    Layout = BoxLayout.Vertical,
                                    Contents = new IFlexComponent[]
                                    {
                                    },
                                    CornerRadius="lg",
                                    Height="12px",
                                    Width="12px",
                                    BorderColor="#EF454D",
                                    BorderWidth="2px"
                                },
                                new FillerComponent()
                                {
                                }
                            },
                            Flex = 0
                        },
                        new TextComponent()
                        {
                            Text = "Akihabara",
                            Gravity = Gravity.Center,
                            Flex = 4,
                            Size = "sm"
                        }
                    },
                    Spacing = "lg",
                    CornerRadius = "30px",
                    Margin = "xl"
                },
                new BoxComponent()
                {
                    Layout = BoxLayout.Horizontal,
                    Contents = new IFlexComponent[]
                    {
                        new BoxComponent()
                        {
                            Layout=BoxLayout.Baseline,
                            Contents = new IFlexComponent[]
                            {
                                new FillerComponent()
                                {
                                }
                            },
                            Flex=1
                        },
                        new BoxComponent()
                        {
                            Layout = BoxLayout.Vertical,
                            Contents = new IFlexComponent[]
                            {
                                new BoxComponent()
                                {
                                    Layout = BoxLayout.Horizontal,
                                    Contents = new IFlexComponent[]
                                    {
                                        new FillerComponent()
                                        {
                                        },
                                        new BoxComponent()
                                        {
                                            Layout= BoxLayout.Vertical,
                                            Contents = new IFlexComponent[]
                                            {
                                            },
                                            Width="2px",
                                            BackgroundColor="#B7B7B7"
                                        },
                                        new FillerComponent()
                                        {
                                        }
                                    },
                                    Flex=1
                                }
                            },
                            Width="12px"
                        },
                        new TextComponent()
                        {
                            Text = "Walk 4min",
                            Gravity = Gravity.Center,
                            Flex=4,
                            Size = "xs",
                            Color = "#8c8c8c"
                        }
                    },
                    Spacing = "lg",
                    Height = "64px"
                },
                new BoxComponent()
                {
                    Layout=BoxLayout.Horizontal,
                    Contents = new IFlexComponent[]
                    {
                        new BoxComponent()
                        {
                            Layout = BoxLayout.Horizontal,
                            Contents = new IFlexComponent[]
                            {
                                new TextComponent()
                                {
                                    Text = "20:34",
                                    Size = "sm",
                                    Gravity = Gravity.Center
                                },
                            },
                            Flex=1
                        },
                        new BoxComponent()
                        {
                            Layout = BoxLayout.Vertical,
                            Contents = new IFlexComponent[]
                            {
                                new FillerComponent()
                                {
                                },
                                new BoxComponent()
                                {
                                    Layout = BoxLayout.Vertical,
                                    Contents = new IFlexComponent[]
                                    {
                                    },
                                    CornerRadius="30px",
                                    Height="12px",
                                    Width="12px",
                                    BorderColor="#6486E3",
                                    BorderWidth="2px"
                                },
                                new FillerComponent()
                                {
                                }
                            },
                            Flex = 0
                        },
                        new TextComponent()
                        {
                            Text = "Ochanomizu",
                            Gravity = Gravity.Center,
                            Flex = 4,
                            Size = "sm"
                        }
                    },
                    Spacing = "lg",
                    CornerRadius = "30px",
                },
                new BoxComponent()
                {
                    Layout = BoxLayout.Horizontal,
                    Contents = new IFlexComponent[]
                    {
                        new BoxComponent()
                        {
                            Layout=BoxLayout.Baseline,
                            Contents = new IFlexComponent[]
                            {
                                new FillerComponent()
                                {
                                }
                            },
                            Flex=1
                        },
                        new BoxComponent()
                        {
                            Layout = BoxLayout.Vertical,
                            Contents = new IFlexComponent[]
                            {
                                new BoxComponent()
                                {
                                    Layout = BoxLayout.Horizontal,
                                    Contents = new IFlexComponent[]
                                    {
                                        new FillerComponent()
                                        {
                                        },
                                        new BoxComponent()
                                        {
                                            Layout= BoxLayout.Vertical,
                                            Contents = new IFlexComponent[]
                                            {
                                            },
                                            Width="2px",
                                            BackgroundColor="#6486E3"
                                        },
                                        new FillerComponent()
                                        {
                                        }
                                    },
                                    Flex=1
                                }
                            },
                            Width="12px"
                        },
                        new TextComponent()
                        {
                            Text = "Metro 1hr",
                            Gravity = Gravity.Center,
                            Flex=4,
                            Size = "xs",
                            Color = "#8c8c8c"
                        }
                    },
                    Spacing = "lg",
                    Height = "64px"
                },
                new BoxComponent()
                {
                    Layout=BoxLayout.Horizontal,
                    Contents = new IFlexComponent[]
                    {
                        new TextComponent()
                        {
                            Text = "20:40",
                            Size = "sm",
                            Gravity = Gravity.Center
                        },
                        new BoxComponent()
                        {
                            Layout = BoxLayout.Vertical,
                            Contents = new IFlexComponent[]
                            {
                                new FillerComponent()
                                {
                                },
                                new BoxComponent()
                                {
                                    Layout = BoxLayout.Vertical,
                                    Contents = new IFlexComponent[]
                                    {
                                    },
                                    CornerRadius = "30px",
                                    Width = "12px",
                                    Height = "12px",
                                    BorderColor="#6486E3",
                                    BorderWidth="2px"
                                },
                                new FillerComponent()
                                {
                                }
                            },
                            Flex=0
                        },
                        new TextComponent()
                        {
                            Text = "Shinjuku",
                            Gravity = Gravity.Center,
                            Flex = 4,
                            Size = "sm"
                        }
                    },
                    Spacing = "lg",
                    CornerRadius = "30px",
                }
            }
        }
    }
}
```

## Json
---
```json
{
  "type": "bubble",
  "size": "mega",
  "header": {
    "type": "box",
    "layout": "vertical",
    "contents": [
      {
        "type": "box",
        "layout": "vertical",
        "contents": [
          {
            "type": "text",
            "text": "FROM",
            "color": "#ffffff66",
            "size": "sm"
          },
          {
            "type": "text",
            "text": "Akihabara",
            "color": "#ffffff",
            "size": "xl",
            "flex": 4,
            "weight": "bold"
          }
        ]
      },
      {
        "type": "box",
        "layout": "vertical",
        "contents": [
          {
            "type": "text",
            "text": "TO",
            "color": "#ffffff66",
            "size": "sm"
          },
          {
            "type": "text",
            "text": "Shinjuku",
            "color": "#ffffff",
            "size": "xl",
            "flex": 4,
            "weight": "bold"
          }
        ]
      }
    ],
    "paddingAll": "20px",
    "backgroundColor": "#0367D3",
    "spacing": "md",
    "height": "154px",
    "paddingTop": "22px"
  },
  "body": {
    "type": "box",
    "layout": "vertical",
    "contents": [
      {
        "type": "text",
        "text": "Total: 1 hour",
        "color": "#b7b7b7",
        "size": "xs"
      },
      {
        "type": "box",
        "layout": "horizontal",
        "contents": [
          {
            "type": "text",
            "text": "20:30",
            "size": "sm",
            "gravity": "center"
          },
          {
            "type": "box",
            "layout": "vertical",
            "contents": [
              {
                "type": "filler"
              },
              {
                "type": "box",
                "layout": "vertical",
                "contents": [],
                "cornerRadius": "30px",
                "height": "12px",
                "width": "12px",
                "borderColor": "#EF454D",
                "borderWidth": "2px"
              },
              {
                "type": "filler"
              }
            ],
            "flex": 0
          },
          {
            "type": "text",
            "text": "Akihabara",
            "gravity": "center",
            "flex": 4,
            "size": "sm"
          }
        ],
        "spacing": "lg",
        "cornerRadius": "30px",
        "margin": "xl"
      },
      {
        "type": "box",
        "layout": "horizontal",
        "contents": [
          {
            "type": "box",
            "layout": "baseline",
            "contents": [
              {
                "type": "filler"
              }
            ],
            "flex": 1
          },
          {
            "type": "box",
            "layout": "vertical",
            "contents": [
              {
                "type": "box",
                "layout": "horizontal",
                "contents": [
                  {
                    "type": "filler"
                  },
                  {
                    "type": "box",
                    "layout": "vertical",
                    "contents": [],
                    "width": "2px",
                    "backgroundColor": "#B7B7B7"
                  },
                  {
                    "type": "filler"
                  }
                ],
                "flex": 1
              }
            ],
            "width": "12px"
          },
          {
            "type": "text",
            "text": "Walk 4min",
            "gravity": "center",
            "flex": 4,
            "size": "xs",
            "color": "#8c8c8c"
          }
        ],
        "spacing": "lg",
        "height": "64px"
      },
      {
        "type": "box",
        "layout": "horizontal",
        "contents": [
          {
            "type": "box",
            "layout": "horizontal",
            "contents": [
              {
                "type": "text",
                "text": "20:34",
                "gravity": "center",
                "size": "sm"
              }
            ],
            "flex": 1
          },
          {
            "type": "box",
            "layout": "vertical",
            "contents": [
              {
                "type": "filler"
              },
              {
                "type": "box",
                "layout": "vertical",
                "contents": [],
                "cornerRadius": "30px",
                "width": "12px",
                "height": "12px",
                "borderWidth": "2px",
                "borderColor": "#6486E3"
              },
              {
                "type": "filler"
              }
            ],
            "flex": 0
          },
          {
            "type": "text",
            "text": "Ochanomizu",
            "gravity": "center",
            "flex": 4,
            "size": "sm"
          }
        ],
        "spacing": "lg",
        "cornerRadius": "30px"
      },
      {
        "type": "box",
        "layout": "horizontal",
        "contents": [
          {
            "type": "box",
            "layout": "baseline",
            "contents": [
              {
                "type": "filler"
              }
            ],
            "flex": 1
          },
          {
            "type": "box",
            "layout": "vertical",
            "contents": [
              {
                "type": "box",
                "layout": "horizontal",
                "contents": [
                  {
                    "type": "filler"
                  },
                  {
                    "type": "box",
                    "layout": "vertical",
                    "contents": [],
                    "width": "2px",
                    "backgroundColor": "#6486E3"
                  },
                  {
                    "type": "filler"
                  }
                ],
                "flex": 1
              }
            ],
            "width": "12px"
          },
          {
            "type": "text",
            "text": "Metro 1hr",
            "gravity": "center",
            "flex": 4,
            "size": "xs",
            "color": "#8c8c8c"
          }
        ],
        "spacing": "lg",
        "height": "64px"
      },
      {
        "type": "box",
        "layout": "horizontal",
        "contents": [
          {
            "type": "text",
            "text": "20:40",
            "gravity": "center",
            "size": "sm"
          },
          {
            "type": "box",
            "layout": "vertical",
            "contents": [
              {
                "type": "filler"
              },
              {
                "type": "box",
                "layout": "vertical",
                "contents": [],
                "cornerRadius": "30px",
                "width": "12px",
                "height": "12px",
                "borderColor": "#6486E3",
                "borderWidth": "2px"
              },
              {
                "type": "filler"
              }
            ],
            "flex": 0
          },
          {
            "type": "text",
            "text": "Shinjuku",
            "gravity": "center",
            "flex": 4,
            "size": "sm"
          }
        ],
        "spacing": "lg",
        "cornerRadius": "30px"
      }
    ]
  }
}
```
