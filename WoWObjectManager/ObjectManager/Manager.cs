﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Magic;
using System.Diagnostics;

namespace WoWObjectManager
{
    class Manager
    {
        internal static BlackMagic WoW;

        internal static IDictionary<ulong, NPCObject> PlayerObjectList = new Dictionary<ulong, NPCObject>();

        internal static uint ObjMgr, CurObj, NextObj;

        public enum Offsets
        {
            clientConnection = 0xE3CB00,
            ObjectManager = 0x462C,
            FirstObject = 0xCC,
            NextObject = 0x34,
            LocalGUID = 0xE0,
            
            //Object
            ObjectType = 0xC,
            ObjectGUID = 0x28,
            Object_X = 0x7F8,
            Object_Y = Object_X + 0x4,
            Object_Z = Object_Y + 0x4,
            Object_NamePointer = 0x974,
            Object_NameOffset = 0x6C,
        }

        internal static void Initialize()
        {
            WoW = new BlackMagic(0000); //EDIT THIS WITH THE WOW PID.

            ObjMgr = WoW.ReadUInt(WoW.ReadUInt((uint) WoW.MainModule.BaseAddress + (uint)Offsets.clientConnection) + (uint)Offsets.ObjectManager);
            CurObj = WoW.ReadUInt(ObjMgr + (Int32)Offsets.FirstObject);

            UInt64 LocalGUID = WoW.ReadUInt64(ObjMgr + (Int32)Offsets.LocalGUID);
            Console.WriteLine(string.Format("Local GUID: {0}", LocalGUID));
            Console.WriteLine(Environment.NewLine);

            RefreshObjectManager();
        }

        internal static void RefreshObjectManager()
        {
            PlayerObjectList.Clear();

            while (CurObj != 0 && (CurObj & 1) == 0)
            {
                /*
                 * 1. Items
                 * 2. Players
                 * 3. NPCS
                 * 4. Containers
                 * 5. Corpses
                 * 6. Game Objects
                 * 7. Dynamic Objects
                 */

                uint ObjectType = WoW.ReadUInt((UInt32) CurObj + (Int32) Offsets.ObjectType);
                uint NextObj = WoW.ReadUInt((UInt32)CurObj + (Int32)Offsets.NextObject);

                //I hate switches.
                if (ObjectType == 3) //NPCs
                {
                    NPCObject NPCObject = new NPCObject(CurObj);
                    Console.WriteLine(string.Format("[NPC] GUID: {0} - X: {1} Y: {2} Z: {3} Name: {4}", NPCObject.GUID, NPCObject.Position.X, NPCObject.Position.Y, NPCObject.Position.Z, NPCObject.Name));

                    PlayerObjectList.Add(NPCObject.GUID, NPCObject);
                }

                CurObj = NextObj;
            }
        }
    }
}
