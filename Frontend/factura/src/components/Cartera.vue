<template>
  <v-layout align-start>
    <v-flex>
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
          </td>
          <td>{{ formatFechEmi(props.item) }}</td>
          <td>{{ formatFechPage(props.item) }}</td>
          <td>{{ props.item.totalFacturado }}</td>
          <td>{{ formatFechDesc(props.item) }}</td>
          <td>{{ props.item.tea }}</td>
          <td>{{ props.item.costesIniciales }}</td>
          <td>{{ props.item.costesFinales }}</td>
          <td>{{ props.item.tcea }}</td>
        </template>
        <template slot="no-data">
          <v-btn color="primary" @click="listar">Resetear</v-btn>
        </template>
      </v-data-table>
    </v-flex>
  </v-layout>

</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      facturas: [],
      dialog: false,
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

    close() {
      this.dialog = false;
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
        if (confirm('Estas seguro de eliminar esta factura?')){
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
  }
};
</script>