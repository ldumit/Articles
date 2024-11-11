namespace Articles.Abstractions.Enums;

public interface IArticle
{
    string Id { get; }
    public ArticleStage StageId{ get; set; }
}