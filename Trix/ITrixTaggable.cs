namespace CVMatrix.Maptrix.Trix;

public interface ITrixTaggable<in TTagType> where TTagType : class, Tags.ITag
{
    public T? GetTag<T>() where T : class, TTagType;
}