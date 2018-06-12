# Elasticsearch-Nest
Elasticsearch: Learning with NEST

## Создание клиента
```csharp
var uri = null;

var settings = new ConnectionSettings(uri)
    .DisableAutomaticProxyDetection()
    .EnableHttpCompression()
    .DisableDirectStreaming()
    .PrettyJson()
    .RequestTimeout(TimeSpan.FromMinutes(2));

Client = new ElasticClient(settings);
```

## Документы

```csharp
public class Book : Model
{
    public string Name { get; set; }

    public int Price { get; set; }

    public int Pages { get; set; }

    public Author Author { get; set; }
}

public class Author : Model
{
    public string Name { get; set; } = "Аноним";

    public DateTime BirthDate { get; set; }
}
```

## Создание и удаление индексов

```csharp
// Создание индекса.
Client.CreateIndex("books", c => c
    .Mappings(m => m.Map<Book>(mp => mp.AutoMap()))
);

// Удаление индекса.
Client.DeleteIndex("books");
```
```json
Valid NEST response built from a successful low level call on PUT: /books?pretty=true
# Request:
{
  "mappings": {
    "book": {
      "properties": {
        "name": {
          "type": "text",
          "fields": {
            "keyword": {
              "type": "keyword",
              "ignore_above": 256
            }
          }
        },
        "price": {
          "type": "integer"
        },
        "pages": {
          "type": "integer"
        },
        "author": {
          "type": "object",
          "properties": {
            "name": {
              "type": "text",
              "fields": {
                "keyword": {
                  "type": "keyword",
                  "ignore_above": 256
                }
              }
            },
            "birthDate": {
              "type": "date"
            },
            "id": {
              "type": "integer"
            }
          }
        },
        "id": {
          "type": "integer"
        }
      }
    }
  }
}
# Response:
{
  "acknowledged" : true,
  "shards_acknowledged" : true,
  "index" : "books"
}
```

## Создание документов

```csharp
var books = new List<Book>
{
    new Book("CLR via C#", 1_500, 900)
    {
        Id = 1,
        Author = new Author("Джеффри Рихтер"),
    },
    new Book("C# 5.0 и платформа .NET", 2_000, 1_200)
    {
        Id = 2,
        Author = new Author("Эндрю Троелсен")
    }
};

foreach (var book in books)
{
    Client.Index(book, i => i
        .Index("books")
        .Type("book")
        .Id(book.Id)
        .Refresh(Refresh.True)
    );
}
```
```json
Valid NEST response built from a successful low level call on PUT: /books/book/1?pretty=true&refresh=true
# Request:
{
  "name": "CLR via C#",
  "price": 1500,
  "pages": 900,
  "author": {
    "name": "Джеффри Рихтер",
    "birthDate": "2048-06-12T18:09:33.2379757+03:00",
    "id": 0
  },
  "id": 1
}
# Response:
{
  "_index" : "books",
  "_type" : "book",
  "_id" : "1",
  "_version" : 1,
  "result" : "created",
  "forced_refresh" : true,
  "_shards" : {
    "total" : 2,
    "successful" : 1,
    "failed" : 0
  },
  "_seq_no" : 0,
  "_primary_term" : 1
}

Valid NEST response built from a successful low level call on PUT: /books/book/2?pretty=true&refresh=true
# Request:
{
  "name": "C# 5.0 и платформа .NET",
  "price": 2000,
  "pages": 1200,
  "author": {
    "name": "Эндрю Троелсен",
    "birthDate": "2048-06-12T18:09:33.2379787+03:00",
    "id": 0
  },
  "id": 2
}
# Response:
{
  "_index" : "books",
  "_type" : "book",
  "_id" : "2",
  "_version" : 1,
  "result" : "created",
  "forced_refresh" : true,
  "_shards" : {
    "total" : 2,
    "successful" : 1,
    "failed" : 0
  },
  "_seq_no" : 0,
  "_primary_term" : 1
}
```

### Обновление документов
### Удаление документов
### Поиск документов и фильтры
### Полнотекстовый поиск
