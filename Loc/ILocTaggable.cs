namespace CVMatrix.Maptrix.Loc;

public interface ILocTaggable<in TTagType> where TTagType : class, Tags.ITag
{
    public T? GetTag<T>() where T : class, TTagType;
}