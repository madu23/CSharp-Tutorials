
using System;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Utilities;

/// <summary>
/// Handles menu selection logic for the restaurant system.
/// </summary>
public class MenuSelectionHandler
{
    // Stores the menu system reference
    private readonly Menu sysMenu;

    // Stores the type of handler being used
    private readonly HandlerType handlerType;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuSelectionHandler"/> class.
    /// </summary>
    /// <param name="sysMenu">The system menu to handle.</param>
    /// <param name="handlerType">The type of handler for menu operations.</param>

    public MenuSelectionHandler(Menu sysMenu, HandlerType handlerType)
    {
        this.sysMenu = sysMenu;
        this.handlerType = handlerType;
    }
}
