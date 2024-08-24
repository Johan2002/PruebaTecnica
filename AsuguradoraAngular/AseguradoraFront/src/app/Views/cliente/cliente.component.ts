import { Component, inject, Input, OnInit } from '@angular/core';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ClienteService } from '../../Services/cliente.service';
import { Router } from '@angular/router';
import { Cliente } from '../../Models/Cliente';

@Component({
  selector: 'app-cliente',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule, MatDatepickerModule, MatNativeDateModule],
  templateUrl: './cliente.component.html',
  styleUrl: './cliente.component.css'
})
export class ClienteComponent implements OnInit {

  // Define una propiedad de entrada que recibe el ID del cliente.
  @Input('id') IdCliente! : number;
  // Inyecta los servicios ClienteService y FormBuilder para usarlos en el componente.
  private clienteServicio = inject(ClienteService);
  public formBuild = inject(FormBuilder);
  // Variable que indica si el formulario está en modo edición o no.
  modoEdicion: boolean = false;

  // Define el formulario reactivo con FormBuilder.
  public formCliente:FormGroup = this.formBuild.group({
    DocumentoIdentidad:[{ value: '', disabled: this.modoEdicion }],
    PrimerNombre:[''],
    SegundoNombre:[''],
    PrimerApellido:[''],
    SegundoApellido:[''],
    Telefono:[''],
    Email:[''],
    FechaNacimiento:[''],
    ValorSeguro:[0],
    Observaciones:['']
  });

  constructor(private router:Router){}

  // Método que se ejecuta cuando el componente se inicializa.
  ngOnInit(): void {
    // Si se ha recibido un ID de cliente distinto de 0, entra en modo edición.
      if(this.IdCliente != 0){
        this.modoEdicion = true;
        // Deshabilita el campo de documento de identidad para que no se pueda modificar.
        this.formCliente.get('DocumentoIdentidad')?.disable();
        // Obtiene los datos del cliente desde el servicio y los carga en el formulario.
        this.clienteServicio.obtener(this.IdCliente).subscribe({
          next:(data) => {
            
            // Actualiza los valores del formulario con los datos del cliente recibido.
            this.formCliente.patchValue({
              DocumentoIdentidad: data.documentoIdentidad,
              PrimerNombre:data.primerNombre,
              SegundoNombre:data.segundoNombre,
              PrimerApellido:data.primerApellido,
              SegundoApellido:data.segundoApellido,
              Telefono:data.telefono,
              Email:data.email,
              FechaNacimiento:data.fechaNacimiento,
              ValorSeguro:data.valorSeguro,
              Observaciones:data.observaciones,
            })
          },
          error:(err) =>{
            console.log(err.message)
          }
        })
      }
  }

  // Método que guarda o edita los datos del cliente según el ID proporcionado.
 guardar(){
    // Crea un objeto cliente con los valores del formulario.
    const cliente:Cliente = {
      idCliente: this.IdCliente,
      documentoIdentidad: this.formCliente.get('DocumentoIdentidad')?.getRawValue(),
      primerNombre: this.formCliente.value.PrimerNombre,
      segundoNombre: this.formCliente.value.SegundoNombre,
      primerApellido: this.formCliente.value.PrimerApellido,
      segundoApellido: this.formCliente.value.SegundoApellido,
      telefono: this.formCliente.value.Telefono,
      email: this.formCliente.value.Email,
      fechaNacimiento: this.formCliente.value.FechaNacimiento,
      valorSeguro: this.formCliente.value.ValorSeguro,
      observaciones: this.formCliente.value.Observaciones,
    }

    // Si el ID del cliente es 0, se crea un nuevo cliente. De lo contrario, se edita el cliente existente.
    if(this.IdCliente == 0){
      this.clienteServicio.crear(cliente).subscribe({
        next:(data) => {
          // Si la creación es exitosa, navega a la página principal. De lo contrario, muestra un mensaje de error.
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }else{
            alert("Error al crear al cliente.");
          }
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }else{
      this.clienteServicio.editar(cliente).subscribe({
        next:(data) => {
          // Si la edición es exitosa, navega a la página principal. De lo contrario, muestra un mensaje de error.
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }else{
            alert("Error al editar al cliente.");
          }
    },
    error:(err)=>{
      console.log(err.message)
    }
  }) 
 }
}

  // Método para volver a la página principal.
  volver(){
    this.router.navigate(["/"]);
  }

}