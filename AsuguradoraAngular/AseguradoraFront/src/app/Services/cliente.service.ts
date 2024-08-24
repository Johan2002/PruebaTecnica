import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { appsettings } from '../Settings/appsettings';
import { Cliente } from '../Models/Cliente';
import { ResponseApi } from '../Models/ResponseApi';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  // Inyecta HttpClient para realizar solicitudes HTTP.
  private http = inject(HttpClient);
  // URL base para acceder a la API de clientes.
  private apiUrl:string = appsettings.apiUrl + "Cliente";

  constructor() { }

  // Método para obtener la lista de clientes desde la API.
  lista(){
    return this.http.get<Cliente[]>(this.apiUrl)
  }

  // Método para obtener un cliente específico por su ID.
  obtener(id: number){
    return this.http.get<Cliente>(`${this.apiUrl}/${id}`)
  }

  // Método para crear un nuevo cliente.
  crear(cliente:Cliente){
    return this.http.post<ResponseApi>(this.apiUrl, cliente)
  }

  // Método para editar un cliente existente.
  editar(cliente:Cliente){
    return this.http.put<ResponseApi>(this.apiUrl, cliente)
  }

  // Método para eliminar un cliente por su ID.
  eliminar(id: number){
    return this.http.delete<ResponseApi>(`${this.apiUrl}/${id}`)
  }
}
