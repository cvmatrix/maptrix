namespace CVMatrix.DropOffDefense.SLib.Core;

public interface ILocTaggable<in TTagType> where TTagType : class, Tags.ILocElementTag
{
    public T? HasTag<T>() where T : class, TTagType;
}