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

            // Register async, to have little performance overhead on every frame individually
            // Shouldn't take too long to run through everything, regardless

            task.Status = "Registering small room faces";
            yield return BaseRoomRegister.Register();

            task.Status = "Registering large room faces";
            yield return LargeRoomRegister.Register();

            task.Status = "Registering scanner room faces";
            yield return MapRoomRegister.Register();
        }
    }
}
