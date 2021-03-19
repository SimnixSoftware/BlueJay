/////////////////////////////////////////////////////////////////////////////
// Global.cs
// 
// Program Tytle:  BlueJay
// Author:  Simon Nixon
// Copyright: 2013 ©
// Description: This file is for application wide methods and properties
//
// IDE: Microsoft Visual Studio 2012 Professional
// Language: C# 4.0
//
// $Id: Global.cs 959 2013-08-23 11:39:26Z Simon $
// $URL: svn://sys2k/svnrepos/Software_Development/Projects/Visual_Studio/BlueJay/trunk/Source/Global.cs $
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Reflection;

namespace BlueJay
{
   /// <summary>
   /// Description of GlobalData.
   /// </summary>
   public class GlobalData
   {
      //==============================   
      // private variables
      //==============================
      private static string _VersionInfo;
      private static bool _DevMode = false;
      private static string _DevelopmentWarning = "";
      
      
      //==============================
      // public properties
      //==============================
      
      public string VersionInfo
      {
         get
         {
            return _VersionInfo;
         }
         set
         {
            _VersionInfo = value;
         }
      }
      
      public bool DevelopmentModeSwitch
      {
         get
         {
            return _DevMode;
         }
         set
         {
            _DevMode = value;
         }
      }
      
      public string DevelopmentWarning
      {
         get
         {
            return _DevelopmentWarning;
         }
         set
         {
            _DevelopmentWarning = value;
         }
      }

      //==============================
      // public methods
      //==============================
      public string GetVersion()
      {
      	if(DevelopmentModeSwitch == true)
      	{
      		VersionInfo = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString()
                  	+"."+ Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()
      		         +"."+ Assembly.GetExecutingAssembly().GetName().Version.Build.ToString()
         			+ DevelopmentWarning;
      	}
      	else
      	{
      		VersionInfo = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString()
                  	+"."+ Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
      	}
         	
        return VersionInfo;
      }
   }
}
