
using System.Data.SQLite;
using System.Data;
public interface IDBReadable 
{
    void InterprateDBData(IDataRecord data);
}
