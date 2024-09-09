namespace Articles.Abstractions;

public interface IArticle
{
    string Id { get; }
    public ArticleStage StageId{ get; set; }
}

public interface IArticleRepository
{
    //public 
}
