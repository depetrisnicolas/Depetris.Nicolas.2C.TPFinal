using Entidades;
/// <summary>
/// Delegado utilizado para buscar un cliente según su dni en una lista de clientes.
/// </summary>
/// <param name="numDni">El dni del cliente que se va a buscar.</param>
/// <param name="listaClientes">La lista de clientes en la que se buscará el cliente.</param>
/// <returns>Un mensaje indicando si la cadena contiene únicamente caracteres alfabéticos o no.</returns>
public delegate Cliente BuscarPorDniDelegate(int numDni, List<Cliente> listaClientes);

