using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagossie
{
    public interface IDataStore<T>
    {
        bool CreateItem(T _item);
        T ReadItem();
        bool UpdateItem(T _item);
        bool DeleteItem(T _item);
    }
}
