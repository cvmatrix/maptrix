namespace CVMatrix.DropOffDefense.SLib.Loc;

public interface ILocTaggable<in TTagType> where TTagType : class, Tags.ILocElementTag
{
    public T? GetTag<T>() where T : class, TTagType;
}