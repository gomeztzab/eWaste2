Sistema de Agentes y Control de Contexto
1. Propósito

El sistema de agentes define módulos independientes encargados de gestionar lógica específica del juego, manteniendo separación de responsabilidades y facilitando:

Escalabilidad
Mantenimiento
Integración con IA (futuro)
Testing modular

Cada agente controla un dominio funcional del juego y se comunica mediante eventos.

2. Arquitectura General
[GameManager]
     │
 ┌───┼───────────────────────────────┐
 │   │                               │
 ▼   ▼                               ▼
InputAgent   GameplayAgent     ProgressionAgent
 │              │                    │
 ▼              ▼                    ▼
UIAgent     WasteAgent         EconomyAgent
 │              │
 ▼              ▼
AudioAgent   ScannerAgent
Principios clave:
Comunicación por eventos (Event Bus)
Bajo acoplamiento
Alta cohesión
Estado centralizado controlado
3. Lista de Agentes
🧠 GameManager (Orquestador Principal)

Responsabilidad:
Controla el estado global del juego.

Estados:

enum GameState {
    Menu,
    Playing,
    Paused,
    GameOver
}

Eventos:

OnGameStart
OnGameOver
OnPause
🎮 InputAgent

Responsabilidad:
Gestiona la entrada táctil del usuario.

Inputs:

Tap
Long Tap
Drag
Double Tap

Output:
Eventos:

OnTap
OnLongTap
OnDrag
OnDoubleTap
⚙️ GameplayAgent

Responsabilidad:
Controla el loop principal:

Recolectar → Escanear → Clasificar

Funciones:

Validar acciones del jugador
Calcular combos
Gestionar errores
♻️ WasteAgent

Responsabilidad:
Gestión de residuos electrónicos.

Datos por objeto:

{
  "id": "phone_01",
  "type": "battery",
  "risk": "high",
  "correctBin": "hazard"
}

Funciones:

Spawn dinámico
Pooling de objetos
Asignación de propiedades
🔍 ScannerAgent

Responsabilidad:
Maneja el escaneo de residuos.

Tipos de escaneo:

Rápido (info parcial)
Completo (info total)

Output:

OnScanSuccess
OnScanFail
🧩 UIAgent

Responsabilidad:
Interfaz de usuario.

Elementos:

Puntaje
Timer
Indicadores de error/acierto
Feedback visual
🔊 AudioAgent

Responsabilidad:
Feedback auditivo.

Eventos:

PlayCorrectSound
PlayErrorSound
PlayComboSound
📈 ProgressionAgent

Responsabilidad:
Sistema de progresión.

Controla:

Dificultad dinámica
Velocidad de spawn
Nuevos tipos de residuos
💰 EconomyAgent

Responsabilidad:
Sistema de recompensas.

Recursos:

Puntos
Moneda virtual

Usos:

Mejoras
Desbloqueos
4. Sistema de Eventos (Event Bus)
Estructura recomendada:
public static class EventBus {
    public static Action OnGameStart;
    public static Action<int> OnScoreUpdated;
    public static Action OnWasteCorrect;
    public static Action OnWasteIncorrect;
}
Ventajas:
Desacoplamiento total
Fácil debugging
Escalable
5. Control de Contexto

Cada agente SOLO debe conocer:

Agente	Contexto permitido
InputAgent	Input del usuario
GameplayAgent	Estado actual del juego
WasteAgent	Datos de residuos
UIAgent	Datos visuales
AudioAgent	Eventos
ProgressionAgent	Métricas de dificultad
❌ Prohibido:
Acceso directo entre agentes
Variables globales sin control
Lógica duplicada
6. Flujo Principal (Ejemplo)
1. WasteAgent genera residuo
2. InputAgent detecta acción
3. ScannerAgent procesa escaneo
4. GameplayAgent valida decisión
5. EconomyAgent actualiza puntos
6. UIAgent muestra feedback
7. AudioAgent reproduce sonido
8. ProgressionAgent ajusta dificultad
7. Persistencia

Sistema recomendado:

PlayerPrefs / JSON / SQLite

Datos a guardar:

Mejor puntaje
Mejoras desbloqueadas
Moneda
8. Escalabilidad futura

Preparado para:

IA adaptativa (dificultad dinámica)
Multijugador (leaderboards)
Nuevos tipos de residuos
Eventos en tiempo real
9. Convenciones
Prefijo por agente:
IA_ → InputAgent
GA_ → GameplayAgent
WA_ → WasteAgent
Eventos:
On[Evento]
Métodos:
Verb + Context
Ej: ProcessScan(), SpawnWaste()
10. Reglas clave

✔ Un agente = una responsabilidad
✔ Todo pasa por eventos
✔ Nada de lógica en UI
✔ Nada de acceso directo entre agentes
✔ Código modular y reutilizable