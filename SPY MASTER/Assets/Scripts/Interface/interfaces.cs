//Jacobo
namespace SpyMaster
{
  ///<sumary>
    ///Declaracion generica de interfaces para enemigos(asignar tipo de valor en la declaracion de clase)
    ///Aplicable a jugador? Profundizar conforme avance el desarrollo.
    ///</sumary>
    public interface IDamageable<T>
    {
        void Damage(T damageTaken);
    }

    public interface IKillable<T>
    {
        void Kill(T damageTaken);
    }
}
