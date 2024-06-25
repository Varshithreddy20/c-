using System;
using System.Collections.Generic;

public class NewsArticle
{
    public string Title { get; }
    public string Category { get; }

    public NewsArticle(string title, string category)
    {
        Title = title;
        Category = category;
    }
}

public class NewsArticleCollection
{
    public List<NewsArticle> articles { get; private set; }

    public delegate void ArticleAddedHandler(object sender, NewsArticle article);
    public delegate void ArticleRemovedHandler(object sender, NewsArticle article);
    public delegate void ArticleFilteredHandler(object sender, string category);

    public event ArticleAddedHandler articleAddedHandler;
    public event ArticleRemovedHandler articleRemovedHandler;
    public event ArticleFilteredHandler articleFilteredHandler;

    public NewsArticleCollection()
    {
        articles = new List<NewsArticle>();
    }

    public void AddArticle(NewsArticle article)
    {
        articles.Add(article);
        articleAddedHandler?.Invoke(this, article);
    }

    public void RemoveArticle(NewsArticle article)
    {
        articles.Remove(article);
        articleRemovedHandler?.Invoke(this, article);
    }

    public List<NewsArticle> FilterArticlesByCategory(string category)
    {
        var filteredArticles = new List<NewsArticle>();
        foreach (var article in articles)
        {
            if (article.Category == category)
            {
                filteredArticles.Add(article);
            }
        }
        articleFilteredHandler?.Invoke(this, category);
        return filteredArticles;
    }

    public void RegisterArticleAddedHandler(ArticleAddedHandler handler)
    {
        articleAddedHandler += handler;
    }

    public void RegisterArticleRemovedHandler(ArticleRemovedHandler handler)
    {
        articleRemovedHandler += handler;
    }

    public void RegisterArticleFilteredHandler(string category, Action<object, string> handler)
    {
        articleFilteredHandler += (sender, cat) =>
        {
            if (cat == category)
            {
                handler(sender, cat);
            }
        };
    }
}

class Program
{
    static void Main(string[] args)
    {
        NewsArticleCollection articleCollection = new NewsArticleCollection();

        articleCollection.RegisterArticleFilteredHandler("Sports", (sender, category) =>
        {
            Console.WriteLine($"Articles filtered by category '{category}'");
        });

        articleCollection.RegisterArticleAddedHandler((sender, article) =>
        {
            Console.WriteLine($"Article added: {article.Title} ({article.Category})");
        });

        articleCollection.RegisterArticleRemovedHandler((sender, article) =>
        {
            Console.WriteLine($"Article removed: {article.Title} ({article.Category})");
        });

        articleCollection.AddArticle(new NewsArticle("New iPhone release", "Technology"));
        articleCollection.FilterArticlesByCategory("Technology");

        articleCollection.AddArticle(new NewsArticle("Record profits for Apple", "Business"));
        articleCollection.FilterArticlesByCategory("Business");

        articleCollection.AddArticle(new NewsArticle("New study shows benefits of exercise", "Health"));
        articleCollection.FilterArticlesByCategory("Health");
        articleCollection.FilterArticlesByCategory("Technology");

        articleCollection.RemoveArticle(new NewsArticle("Record profits for Apple", "Business"));
        articleCollection.FilterArticlesByCategory("Business");
    }
}
