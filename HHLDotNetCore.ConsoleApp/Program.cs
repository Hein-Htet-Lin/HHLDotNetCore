﻿// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.Common;
using HHLDotNetCore;
using HHLDotNetCore.ConsoleApp;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

Console.WriteLine("Hello, World!");



// AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Delete();

// DapperExample dapperExample = new DapperExample();
// dapperExample.Create("Happy Sex Day","Fucker","Fuck Fuck Fuck");
// dapperExample.Edit(6);
// dapperExample.Update(7,"Soe Aye","Leo Leo","Lion");
// dapperExample.Delete(11);


// EFCoreExample efCoreExample = new EFCoreExample();
// efCoreExample.Read();
// efCoreExample.Create("Mingalar pr Taw Tar Myar","Takhin Latt","Taw Tar chay mone yay");
// efCoreExample.Edit(1);
// efCoreExample.Update(1,title: "",author:"U Ba",content:"");
// efCoreExample.Delete(10);

AdoDotNetExample2 adoDotNetExample2 = new AdoDotNetExample2();
adoDotNetExample2.Edit();