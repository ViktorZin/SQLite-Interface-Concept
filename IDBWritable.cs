
public interface IDBWritable 
{
    string TableName { get; set; }
    string TableLegend { get; set; }
    void WriteClassToDB();

    void SetDBData();
}
