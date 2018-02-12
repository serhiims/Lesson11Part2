//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MovedComponent moved { get { return (MovedComponent)GetComponent(GameComponentsLookup.Moved); } }
    public bool hasMoved { get { return HasComponent(GameComponentsLookup.Moved); } }

    public void AddMoved(bool newValue) {
        var index = GameComponentsLookup.Moved;
        var component = CreateComponent<MovedComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMoved(bool newValue) {
        var index = GameComponentsLookup.Moved;
        var component = CreateComponent<MovedComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMoved() {
        RemoveComponent(GameComponentsLookup.Moved);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherMoved;

    public static Entitas.IMatcher<GameEntity> Moved {
        get {
            if (_matcherMoved == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Moved);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMoved = matcher;
            }

            return _matcherMoved;
        }
    }
}
