# BclExtensionPack

## using Task Extensions
### Taskを返すWhenAll拡張メソッド（=同期版でいうところのvoid）
`await hoges.Select(async hoge => { Console.WriteLine(await GetHogeNameAsync(hoge)); }).WhenAll();`

### Task<T[]>を返すWhenAll拡張メソッド
`var fugas = await hoges.Select(async x => new { Id = x, Name = await GetNameAsync(x) }).WhenAll();`

## ValueTupleのWhenAll拡張メソッド(2-8組版まで対応)
`var (hoge, fuga, piyo) = await (GetHogeAsync(), GetFugaAsync(), GetPiyoAsync()).WhenAll();`

## using UriBuilder Extensions
パスを連結して作りたいケースに対応する。

`var hoge = new UriBuilder("www.hoge.co.jp/").UseScheme("https").SetPort(5001).AppendPath("fuga").AppendPath("entry");`

## using Enumerable Extensions
### AsList(Listにcast出来る場合はcastし、そうでなければToListする。)
`var hogeList = hoges.AsList();`

## using IsAny<T>(non null且つ、要素が存在するかの検査を行い結果をboolで返却する。)
`if(hoges.IsAny()){ }`  
`if(hoges.IsAny(hoge => hoge is { fuga : not null })){ }`