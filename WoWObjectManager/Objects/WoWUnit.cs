﻿/*
 * This file is part of the WoWObjectManager (C) 2013 Finn Grimpe
 * Copyright 2013 Finn Grimpe, All Rights Reserved
 * 
 * Github:  https://github.com/finndev/WoWObjectManager/
 * Website: http://finn.lu/
 * License: http://finn.lu/license
 *
 */

using System;

namespace WoWObjectManager
{
    /// <summary>
    /// An unit (Monster, NPCs)
    /// </summary>
    class WoWUnit : WoWObject
    {
        /// <summary>
        /// Instantiates a new WoWUnit
        /// </summary>
        /// <param name="BaseAddress">The units base address.</param>
        public WoWUnit(uint BaseAddress)
            : base(BaseAddress) { }

        /// <summary>
        /// Returns the postion as Vector3.
        /// </summary>
        internal Vector3 Position
        {
            get
            {
                return new Vector3(
                    ObjectManager.WoW.ReadFloat(BaseAddress + (Int32) Offsets.WoWUnit.X),
                    ObjectManager.WoW.ReadFloat(BaseAddress + (Int32) Offsets.WoWUnit.Y),
                    ObjectManager.WoW.ReadFloat(BaseAddress + (Int32) Offsets.WoWUnit.Z)
                    );
            }
        }

        /// <summary>
        /// The units name.
        /// </summary>
        internal string Name
        {
            get { return ObjectManager.WoW.ReadASCIIString(ObjectManager.WoW.ReadUInt(ObjectManager.WoW.ReadUInt(BaseAddress + (Int32)Offsets.WoWUnit.NamePointer) + (Int32) Offsets.WoWUnit.NameOffset), 100); }
        }

        internal int DisplayId
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddress + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.DisplayId); }
        }

        /// <summary>
        /// The units base health
        /// </summary>
        internal float BaseHealth
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddress + (Int32)Offsets.Descriptors.Descriptor) + (Int32) Offsets.Descriptors.BaseHealth); }
        }

        /// <summary>
        /// The units max health
        /// </summary>
        internal float MaxHealth {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddress + (Int32)Offsets.Descriptors.Descriptor) + (Int32) Offsets.Descriptors.MaxHealth); }
        }

        /// <summary>
        /// The units base power
        /// </summary>
        internal float BasePower
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddress + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.BasePower); }
        }

        /// <summary>
        /// The units max power
        /// </summary>
        internal float MaxPower
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddress + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.MaxPower); }
        }

        /// <summary>
        /// The units level
        /// </summary>
        internal float Level
        {
            get { return ObjectManager.WoW.ReadInt(ObjectManager.WoW.ReadUInt((uint)BaseAddress + (Int32)Offsets.Descriptors.Descriptor) + (Int32)Offsets.Descriptors.Level); }
        }

        /// <summary>
        /// Returns whether the unit is alive or not.
        /// </summary>
        internal bool IsAlive
        {
            get
            {
                if (BaseHealth > 0)
                    return true;
                return false;
            }
        }
    }

}
