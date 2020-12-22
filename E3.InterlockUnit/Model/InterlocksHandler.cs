using E3.InterlockUnit.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace E3.InterlockUnit.Model
{
    public class InterlocksHandler : IInterlocksHandler
    {
        public InterlocksHandler(IUnityContainer containerProvider)
        {
            //Resolve Field devices communicator   

            Task.Factory.StartNew(new Action(() => Thread.Sleep(250))).ContinueWith((t) => CheckForInterlocksMet());
        }

        private void CheckForInterlocksMet()
        {
            foreach (Interlock interlock in SystemInterlocks)
            {
                foreach (Dependency dependency in interlock.Dependencies)
                {
                    //get the dependency property value from FieldDevicesCommunicator
                    // and compute the interlock result using dependency type
                }
                if (interlock.Result)
                {
                    UpdateInterlockTargets?.BeginInvoke(interlock.DeviceId, interlock.Targets, null, null);
                }
            }
        }

        public void AddInterlock(Interlock interlock)
        {
            if (SystemInterlocks.Any(item => item == interlock))
            {
                throw new Exception("Interlock already exists");
            }
            else
            {
                SystemInterlocks.Add(interlock);
            }
        }

        public IList<Interlock> GetAllInterlocks()
        {
            return SystemInterlocks;
        }

        public void RemoveInterlock(Interlock interlock)
        {
            if (SystemInterlocks.Any(item => item == interlock))
            {
                SystemInterlocks.Remove(interlock);
            }
            else
            {
                throw new Exception("Interlock does not exist");
            }
        }

        public IList<Interlock> SystemInterlocks { get; private set; }

        public event UpdateInterlockTargetsEventHandler UpdateInterlockTargets;

    }
}
