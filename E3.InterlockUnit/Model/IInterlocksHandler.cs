using E3.InterlockUnit.Model.Data;
using System.Collections.Generic;

namespace E3.InterlockUnit.Model
{
    public interface IInterlocksHandler
    {
        void AddInterlock(Interlock interlock);

        void RemoveInterlock(Interlock interlock);

        IList<Interlock> GetAllInterlocks();

        IList<Interlock> SystemInterlocks { get; }

        event UpdateInterlockTargetsEventHandler UpdateInterlockTargets;
    }

    public delegate void UpdateInterlockTargetsEventHandler(string deviceId, IList<Property> targets);
}
