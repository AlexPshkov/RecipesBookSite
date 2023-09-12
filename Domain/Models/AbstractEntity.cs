using System.Reflection;
using System.Text;

namespace Domain.Models;

public class AbstractEntity
{

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        foreach ( FieldInfo fieldInfo in GetType().GetFields() )
        {
            stringBuilder.Append( fieldInfo.Name ).Append( ": " ).Append( fieldInfo.GetValue( this ) ).Append( "   " );
        }
        
        return stringBuilder.ToString();
    }
}