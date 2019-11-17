<template>
  <v-layout align-start>
    <v-flex v-if="logeado">
      <v-toolbar flat color="white">
        <v-toolbar-title>Facturas</v-toolbar-title>
        <v-divider class="mx-2" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-text-field
          class="text-xs-center"
          v-model="search"
          append-icon="search"
          label="Búsqueda"
          single-line
          hide-details
        ></v-text-field>
        <v-spacer></v-spacer>
      </v-toolbar>
      <v-data-table :headers="headers" :items="facturas" :search="search" class="elevation-1">
        <template slot="items" slot-scope="props">
          <td class="justify-center layout px-0">
            <v-icon small class="mr-2" @click="deleteFactura(props.item.id)">delete</v-icon>
            <v-icon small class="mr-2" @click="agregarFactura(props.item)">add</v-icon>
          </td>
          <td>{{ formatFechEmi(props.item) }}</td>
          <td>{{ formatFechPage(props.item) }}</td>
          <td>{{ props.item.totalFacturado }}</td>
          <td>{{ formatFechDesc(props.item) }}</td>
          <td>{{ props.item.tea * 100}}%</td>
          <td>{{ props.item.costesIniciales }}</td>
          <td>{{ props.item.costesFinales }}</td>
          <td>{{ props.item.tcea * 100}}%</td>
        </template>
        <template slot="no-data">
          <v-btn color="primary" @click="listar">Resetear</v-btn>
        </template>
      </v-data-table>
      <v-container grid-list-sm class="pa-4 white">
            <h3>Seleccionados: </h3>
            <v-layout row wrap>
                <v-flex xs12 sm12 md12 lg12 xl12>
                    <v-data-table :headers="headers" :items="carritoFact" hide-actions class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td class="justify-center layout px-0">
                                <v-icon small class="mr-2" @click="removerFactura(carritoFact,props.item)">
                                    delete
                                </v-icon>
                            </td>
                            <td>{{ formatFechEmi(props.item) }}</td>
                            <td>{{ formatFechPage(props.item) }}</td>
                            <td>{{ props.item.totalFacturado }}</td>
                            <td>{{ formatFechDesc(props.item) }}</td>
                            <td>{{ props.item.tea * 100}}%</td>
                            <td>{{ props.item.costesIniciales }}</td>
                            <td>{{ props.item.costesFinales }}</td>
                            <td>{{ props.item.tcea * 100}}%</td>
                        </template>
                        <template slot="no-data">
                            <h3>No hay facturas agregadas.</h3>
                        </template>
                    </v-data-table>
                </v-flex>

                <v-flex xs12 sm12 md12 lg12 xl12>
                    <v-btn @click="limpiarCarrito()" color="blue darken-1" flat>Limpiar</v-btn>
                    <v-btn @click="calcularTCEA()" color="success">Calcular TCEA (Cartera)</v-btn>
                </v-flex>
            </v-layout>
        </v-container>
        <v-container grid-list-sm class="pa-4 white" v-if="resp">
          <h2>La TCEA de la cartera es: {{ this.TCEA * 100 }} %</h2>
        </v-container>
    </v-flex>
    <v-container v-if="!logeado">
    <v-layout
      text-xs-center
      wrap
    >
      <v-flex xs12>
        <v-img
          src="https://cdn.pixabay.com/photo/2014/04/02/10/26/attention-303861_960_720.png"
          class="my-3"
          contain
          height="200"
        ></v-img>
      </v-flex>

      <v-flex mb-4>
        <h1 class="display-2 font-weight-bold mb-3">
          Necesitas iniciar sesión
        </h1>
        <p class="subheading font-weight-regular">
          Para iniciar sesion de click
          <a href="/login">aquí</a>
          <br>Para registrarse de click
          <a href="/registro">aquí</a>
        </p>
      </v-flex>
      
    </v-layout>
  </v-container>
  </v-layout>

</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      logeado: localStorage.getItem('logeado'),
      facturas: [],
      carritoFact: [],
      dialog: false,
      resp: false,
      mucho: false,
      headers: [
        { text: "Opciones", value: "opciones", sortable: false },
        { text: "FechaEmision", value: "fechaEmision", sortable: false },
        { text: "FechaPago", value: "fechaPago", sortable: false },
        { text: "TotalFacturado", value: "totalFacturado", sortable: false },
        { text: "FechaDescuento", value: "fechaDescuento", sortable: false },
        { text: "TEA", value: "tea", sortable: false },
        { text: "CostesIniciales", value: "costesIniciales", sortable: false },
        { text: "CostesFinales", value: "costesFinales", sortable: false },
        { text: "TCEA", value: "tcea", sortable: false }
      ],
      search: "",

      //Model
      id: "",
      FechaEmision: "",
      FechaPago: "", 
      TotalFacturado: 0, 
      FechaDescuento: "", 
      TEA: 0, 
      CostesIniciales: 0, 
      CostesFinales: 0,
      TCEA: 0,
      UsuarioId: localStorage.getItem('id')  
    };
  },

  watch: {
    dialog(val) {
      val || this.close();
    }
  },

  created() {
    this.listar();
  },
  methods: {
    listar() {
      let me = this;
      axios
        .get("api/Factura/user/"+me.UsuarioId)
        .then(function(response) {
          //console.log(response);
          me.facturas = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    eliminar (item) {
        const index = this.desserts.indexOf(item)
        confirm('Are you sure you want to delete this item?') && this.desserts.splice(index, 1)
      },
    limpiarCarrito(){
        this.resp = false
        this.carritoFact = []
    },
    close() {
      this.dialog = false;
    },
    removerFactura(arr, item) {
            this.resp = false
            var i = arr.indexOf(item);
            if (i !== -1) {
                arr.splice(i, 1);
            }
    },
    calcularTCEA(){
        if(this.carritoFact.length>0){
          this.resp = true
          this.TCEA = 0
          for (let i = 0; i < this.carritoFact.length; i++) {
            this.TCEA += this.carritoFact[i].tcea
          }
          this.TCEA = this.TCEA / this.carritoFact.length
        } else {
          alert('No hay elementos para calcular la TCEA de la cartera.')
        }
    },
    formatFechEmi(item){
      var fecha=new Date(item.fechaEmision);
      return fecha.getFullYear()+"-"+fecha.getMonth()+"-"+fecha.getDate();
    },
    formatFechPage(item){
      var fecha=new Date(item.fechaPago);
      return fecha.getFullYear()+"-"+fecha.getMonth()+"-"+fecha.getDate();
    },
    formatFechDesc(item){
      var fecha=new Date(item.fechaDescuento);
      return fecha.getFullYear()+"-"+fecha.getMonth()+"-"+fecha.getDate();
    },

    deleteFactura(index){
        if (confirm('¿Estas seguro de eliminar esta factura?')){
           let me = this;
      axios
        .delete("api/Factura/"+index
        )
        .then(function(response) {
          console.log(response.data);
          me.listar()
       })
        .catch(function(error) {
            alert("Hubo un error al eliminar una factura")
        });}

      },
    agregarFactura(item){
      this.mucho = false
      for (let i = 0; i < this.carritoFact.length; i++) {
        if (item.id == this.carritoFact[i].id){
            this.mucho = true
        }
      }
      if(!this.mucho){
        this.resp = false
        this.carritoFact.push(item);
      } else {
        alert('La factura ya esta agregada.')
      }
    }
  }
};
</script>