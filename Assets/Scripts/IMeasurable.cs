using System;

public interface IMeasurable
{
    public float GetResistance();
    //public float GetCurrent();
    //public float GetVoltage();
    public float GetPower();
}