import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ClienteService } from '../../Services/cliente.service';
import { Cliente } from '../../Models/Cliente';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inicio',
  standalone: true,
  imports: [MatCardModule, MatTableModule, MatIconModule, MatButtonModule, MatPaginatorModule, MatFormFieldModule, MatInputModule],
  templateUrl: './inicio.component.html', 
  styleUrl: './inicio.component.css',
})
export class InicioComponent{

  // Inyecta el servicio ClienteService para manejar operaciones relacionadas con los clientes.
  private clienteServicio = inject(ClienteService);
  // Lista de clientes que se mostrará en la tabla.
  public listaClientes: Cliente[] = [];
  // Define las columnas que se mostrarán en la tabla de datos.
  public displayedColumns: string[] = [
    'DocumentoIdentidad',
    'PrimerNombre',
    'SegundoNombre',
    'PrimerApellido',
    'SegundoApellido',
    'Telefono',
    'Email',
    'FechaNacimiento',
    'ValorSeguro',
    'Observaciones',
    'Accion'
  ];

  // Define el dataSource para la tabla, usando MatTableDataSource para manejar datos y filtrado.
  public dataSource = new MatTableDataSource<Cliente>([]);
  // Referencia al componente MatPaginator para la paginación en la tabla.
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  // Constructor que inyecta el servicio Router para la navegación.
  constructor(private router:Router){
    // Llama al método para obtener la lista de clientes al inicializar el componente.
    this.obtenerCliente();

  }

  // Método para obtener la lista de clientes desde el servicio y actualizar el dataSource.
  obtenerCliente(){
    this.clienteServicio.lista().subscribe({
      next:(data)=>{
        if(data.length > 0){
          console.log('Datos recibidos:', data);
          // Actualiza el dataSource con los datos recibidos.
          this.dataSource.data = data;
          // Asocia el paginator con el dataSource.
          this.dataSource.paginator = this.paginator;
        }
      },
      error:(err)=>{
        console.log(err.message);
      }
    })
  }

  // Método para aplicar un filtro al dataSource basado en el valor ingresado en el campo de filtro.
  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    console.log('Filtro aplicado:', filterValue);
    // Aplica el filtro al dataSource.
    this.dataSource.filter = filterValue;

    // Reinicia el paginador a la primera página cuando se aplica un filtro.
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  // Método para navegar a la página de creación de un nuevo cliente.
  nuevo(){
    this.router.navigate(['/cliente', 0]);
  }

  // Método para navegar a la página de edición de un cliente específico.
  editar(cliente: Cliente) {
    console.log(cliente.idCliente)
    if (cliente && typeof cliente.idCliente === 'number') {
      // Navega a la página de edición del cliente usando su ID.
      this.router.navigate(['/cliente', cliente.idCliente]);
    } else {
      alert('El ID del cliente es inválido. Por favor, intenta nuevamente.');
    }
  }

  // Método para eliminar un cliente después de confirmar la acción.
  eliminar(cliente: Cliente) {
    console.log(cliente.idCliente)
    if (cliente && cliente.idCliente) {
      // Confirma la eliminación del cliente.
      if (confirm(`¿Está seguro de eliminar al cliente ${cliente.primerNombre} ${cliente.primerApellido} (ID: ${cliente.documentoIdentidad})?`)) {
        this.clienteServicio.eliminar(cliente.idCliente).subscribe({
          next: (data) => {
            if (data.isSuccess) {
              this.obtenerCliente(); // Recarga la lista de clientes después de la eliminación.
              alert('Cliente eliminado correctamente.');
            } else {
              alert('No se pudo eliminar el cliente. Intente nuevamente.');
            }
          },
          error: (err) => {
            if (err.status === 404) {
              alert('El cliente no existe.');
            } else {
              console.error('Error al eliminar el cliente:', err);
              alert('Ocurrió un error inesperado.');
            }
          }
        });
      }
    } else {
      console.error('El ID del cliente es inválido.');
      alert('Por favor, seleccione un cliente válido.');
    }
  }

}
