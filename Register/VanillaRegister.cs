using Nautilus.Handlers;
using System.Collections;

namespace ModularilyBased.Register
{
    internal static class VanillaRegister
    {
        private static bool patched = false;

        public static IEnumerator Register(WaitScreenHandler.WaitScreenTask task)
        {
            if (patched)
            {
                yield break;
            }
            patched = true;

            task.Status = "Registering small room faces";
            yield return BaseRoomRegister.Register();
        }
    }
}
