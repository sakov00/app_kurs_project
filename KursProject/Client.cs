//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KursProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int id { get; set; }
        public Nullable<int> Number_Group { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual DataClient DataClient { get; set; }
    }
}
