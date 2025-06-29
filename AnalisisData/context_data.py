class ContextData:
    """Abstract class with the main structure of a context data class
    A context data has:
    - A reference to the context stack to push new states
    or pop itself
    - A reference to the parent context, the context that created this.
    It is useful to notify the parent that the child context is removed
    from the context stack

    ContextData contains two important methods:
    - parseEvent: it decides what to do with the current event. This event can
    be consumed (or not)
    - onPopChildContext: what to do when a child context has been removed
    from the context stack
    """

    def __init__(self, contextStack, parentContext = None) -> None:
        """Constructor
        A parent context can be None
        """
        self.contextStack = contextStack
        self.parentContext = parentContext

    def parseEvent(self, event) -> bool:
        """Parses the event.
        Return True if the event must be consumed
        """
        pass

    def popContext(self) -> None:
        """It is the best way to remove itself from the context stack
        because this method notifies the paren context (if exists) that
        the top context has been removed
        """
        childContext = self.contextStack.pop()
        if self.parentContext is not None:
            self.parentContext.onPopChildContext(childContext)        
    
    def onPopChildContext(self,childContextData) -> None:
        """What to do when a child context has been removed from the stack
        
        Keyword arguments:
        childContextData -- The context removed from the stack 
        """
        pass


class RootContextData(ContextData):
    """This is the first context data in the context stack
    It creates game contexts and stores them 
    """

    def __init__(self, contextStack, parentContext=None) -> None:
        """Constructor
        """
        super().__init__(contextStack, parentContext)
        self.games = []
    
    def parseEvent(self, event) -> bool:
        """Creates a new Session context on GAME:START event
        """
        if event['event_type'] == 0:
           self.contextStack.append(GameContextData(self.contextStack, self))
           # The event is not consumed because it will be used by the game context
           return False
        return True

    def onPopChildContext(self,childContextData) -> None:
        """Stores the session data when removed from the stack
        """
        self.games.append(childContextData)


class GameContextData(ContextData):
    """Represents a game session (when the user starts a new game until wins or fails)
    It stores when a game starts and finishes, its length and the information 
    extracted from the levels played during the game session
    """
    
    def __init__(self, contextStack, parentContext=None) -> None:
        """Constructor
        """
        super().__init__(contextStack, parentContext)
        self.tsGameStart = None
        self.tsGameEnd = None
        self.gameSessionLengthMs = 0
        self.levels = []

    def parseEvent(self, event) -> bool:
        """It stores information on GAME:START and GAME:END events.
        It also creates a LevelContext when a level starts
        """
        if event['event_type'] == 2:
           # Create a level context and don't consume the event
           self.contextStack.append(LevelContextData(self.contextStack, self))
           return False
        elif event['event_type'] == 0:
           self.tsGameStart = event['event_timestamp']
        elif (event['event_type'] == 1):
            self.tsGameEnd = event['event_timestamp'] 
            self.gameSessionLengthMs = self.tsGameEnd - self.tsGameStart
            self.popContext()
        return True
    
    def onPopChildContext(self,childContextData) -> None:
        """Stores a level context when it is removed from the stack
        """
        self.levels.append(childContextData)


class LevelContextData(ContextData):
    """It stores information about level events:
    - When the level starts and ends and its lenght
    - The event result
    - Player deaths during this level
    """
    def __init__(self, contextStack, parentContext=None) -> None:
        """Constructor
        """
        super().__init__(contextStack, parentContext)
        self.id = None
        self.tsLevelStart = None
        self.tsLevelEnd = None
        self.levelLengthMs = 0
        self.levelResult = None    
        self.deaths = []    
        self.gravity = []
        self.freeze = []
        self.freeze_start_data  = None

    def parseEvent(self, event) -> bool:
        """It stores data on LEVEL:START and LEVEL:END
        """
        if event['event_type'] == 2:
            self.id = event["level_id"]
            self.tsLevelStart = event['event_timestamp']       
        elif (event['event_type'] == 3) and (self.id == event["level_id"]):
            self.tsLevelEnd = event['event_timestamp'] 
            self.levelLengthMs = self.tsLevelEnd - self.tsLevelStart
            self.levelResult = event["level_win"]
            self.popContext()
        elif (event['event_type'] == 4):
            self.gravity.append( dict(x=event['position_x'], y=event['position_y'], direction=event['direction'], timestamp=event['event_timestamp']))
        elif (event['event_type'] == 5):
            # Guardamos el inicio del cambio de gravedad
            self.freeze_start_data = { "ts": event['event_timestamp'], "x": event['position_x'], "y": event['position_y'] }
        elif (event['event_type'] == 6):
            # Si hay inicio, calculamos duración y guardamos junto a la posición
            if self.freeze_start_data:
                duration = event['event_timestamp'] - self.freeze_start_data["ts"]
                self.freeze.append(dict(duration=duration, x=self.freeze_start_data["x"], y=self.freeze_start_data["y"], timestamp=self.freeze_start_data["ts"]))
                self.freeze_start_data = None
        elif (event['event_type'] == 7):
            self.deaths.append( dict( x=event['position_x'], y=event['position_y'], dt=event['death_type'], timestamp=event['event_timestamp']))
    
        return True