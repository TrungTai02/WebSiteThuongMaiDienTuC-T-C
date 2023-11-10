//// -----------------------------------------------------------------------
//// <copyright file="App_Start.cs" company="Fluent.Infrastructure">
////     Copyright © Fluent.Infrastructure. All rights reserved.
//// </copyright>
////-----------------------------------------------------------------------
/// See more at: https://github.com/dn32/Fluent.Infrastructure/wiki
////-----------------------------------------------------------------------

using Fluent.Infrastructure.FluentTools;
using ThuongMaiDienTu.DataBase;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ThuongMaiDienTu.App_Start), "PreStart")]

namespace ThuongMaiDienTu {
    public static class App_Start {
        public static void PreStart() {
           
        }
    }
}