using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /*
     * Diese Klasse soll nicht in das Model aufgenommen werden.
     * Damit man dies ausschließen kann, kann man entweder die
     * folgende Annotation verwenden oder eine einstellung der
     * Contextklasse vornehmen.
     */
    //[NotMapped]
    public class MyInMemoryOnlyClass { }
}
