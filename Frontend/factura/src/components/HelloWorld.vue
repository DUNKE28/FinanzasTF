<template>
  <v-container>
    <v-layout
      text-xs-center
      wrap
    >
      <v-flex xs12>
        <v-img
          src="https://i.imgur.com/dsG33if.png"
          class="my-3"
          contain
          height="200"
        ></v-img>
      </v-flex>

      <v-flex mb-4>
        <h1 class="display-2 font-weight-bold mb-3">
          Bienvenido a FacturaInc
        </h1>
        <p class="subheading font-weight-regular">
          Nuestra meta es hacer el calculo de tus descuentos por
          <br>factura más facil de manera intuitiva.
        </p>
      </v-flex>

      <v-flex
        mb-5
        xs12
      >
        <h2 class="headline font-weight-bold mb-3">¿Quienes somos?</h2>

        <p class="subheading font-weight-regular">
          Somos estudiantes de la facultad de ingenieria de la Universidad
          <br>Peruana de Ciencias Aplicadas (UPC). Buscamos ofrecer un
          <br>servicio de calidad y eficiente a todos nuestros usuarios.
        </p>
      </v-flex>
      
    </v-layout>
  </v-container>
</template>

<script>
  export default {
    data: () => ({
      
    })
  }
</script>

<style>

</style>
