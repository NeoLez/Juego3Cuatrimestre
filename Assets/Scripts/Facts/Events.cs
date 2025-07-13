namespace Facts {
    public static class Events {
        public static readonly GameEvent<Unit> ON_PLAYER_JUMPED = new();
        public static readonly GameEvent<Unit> ON_PLAYER_USE_DASH = new();
        public static readonly GameEvent<Unit> ON_PLAYER_USE_DASH_SELF = new();
        public static readonly GameEvent<float> ON_PLAYER_WALKED = new();
    }
}