using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpSample.Recordable
{
    [Recordable]
    public class Invoice
    {
        [Reference]
        public Customer Customer { get; set; }

        [Child]
        public Address Address { get; set; }

    }
    [Recordable]
    public class Address
    {

        public string Street { get; set; }
    }
    [Recordable]
    public class Customer
    {
        public string Name { get; set; }
    }

    public class RecordableTest
    {
        private static Invoice _invoice;
        public static void Test()
        {
            _invoice = new Invoice();

            _invoice.Address = new Address()
            {
                Street = "Street 01"
            };
            _invoice.Address.Street = "Street 02";

            Undo();

            Console.WriteLine("Undo:" + _invoice.Address.Street);

            Redo();
            Console.WriteLine("Redo:" + _invoice.Address.Street);
        }

        private static void Redo()
        {
            var recorder = RecordingServices.DefaultRecorder;
            if (recorder.UndoOperations.Count > 0)
            {
                recorder.Redo();
            }
        }

        private static void Undo()
        {
            var recorder = RecordingServices.DefaultRecorder;
            if (recorder.UndoOperations.Count > 0)
            {
                recorder.Undo();
            }
        }
    }
}
