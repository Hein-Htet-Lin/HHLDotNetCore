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
// dapperExample.Edit(7);

EFCoreExample efCoreExample = new EFCoreExample();
// efCoreExample.Read();
efCoreExample.Create("Mingalar pr Taw Tar Myar","Takhin Latt","Taw Tar chay mone yay");
