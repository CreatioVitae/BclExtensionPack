# BclExtensionPack

## using Task Extensions
Todo:

## using UriBuilder Extensions
パスを連結して作りたいケースに対応する。

`var hoge = new UriBuilder("www.hoge.co.jp/").UseScheme("https").SetPort(5001).AppendPath("fuga").AppendPath("entry");`