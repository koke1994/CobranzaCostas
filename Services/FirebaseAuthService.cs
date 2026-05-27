using Plugin.Firebase.Auth;

namespace CobranzaCostas.Services;

/// <summary>
/// Encapsula toda la interacción con Firebase Authentication.
/// </summary>
public class FirebaseAuthService
{
    /// <summary>
    /// Autentica al usuario con correo y contraseña.
    /// </summary>
    /// <returns>
    /// El UID del usuario si el login es exitoso.
    /// Null si las credenciales son incorrectas o hay un error de red.
    /// </returns>
    public async Task<string?> LoginAsync(string email, string password)
    {
        try
        {
            var result = await CrossFirebaseAuth.Current
                .SignInWithEmailAndPasswordAsync(email, password);

            return result.User?.Uid;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FirebaseAuth] Error en login: {ex.Message}");
            return null;
        }
    }

    /// <summary>Cierra la sesión activa en Firebase Auth.</summary>
    public async Task LogoutAsync()
    {
        try
        {
            await CrossFirebaseAuth.Current.SignOutAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FirebaseAuth] Error en logout: {ex.Message}");
        }
    }

    /// <summary>Devuelve el UID del usuario actualmente autenticado, o null si no hay sesión.</summary>
    public string? GetCurrentUserId() =>
        CrossFirebaseAuth.Current.CurrentUser?.Uid;

    public bool IsLoggedIn =>
        CrossFirebaseAuth.Current.CurrentUser is not null;
}
