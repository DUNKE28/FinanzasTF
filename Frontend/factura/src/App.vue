<template>
  <v-app id="app">
    <v-navigation-drawer
      fixed
      :clipped="$vuetify.breakpoint.mdAndUp"
      app
      v-model="drawer"
    >
      <v-list dense>
        <template>
          <v-list-tile :to="{name:'home'}">
            <v-list-tile-action>
              <v-icon>home</v-icon>
            </v-list-tile-action>
            <v-list-tile-title>
              Inicio
            </v-list-tile-title>
          </v-list-tile>
        </template>

           
            <v-list-tile :to="{ name: 'facturas'}" v-if="logeado">
              <v-list-tile-action>
                <v-icon>insert_drive_file</v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>
                  Factura
                </v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
            <v-list-tile :to="{ name: 'carteras'}" v-if="logeado">
              <v-list-tile-action>
                <v-icon>file_copy</v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>
                  Cartera
                </v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>

        <template v-if="!logeado">
          <v-list-tile :to="{name:'login'}">
            <v-list-tile-action>
              <v-icon class="material-design">account_circle</v-icon>
            </v-list-tile-action>
            <v-list-tile-title>
              Iniciar Sesion
            </v-list-tile-title>
          </v-list-tile>
        </template>

        <template v-if="!logeado">
          <v-list-tile :to="{name:'registro'}">
            <v-list-tile-action>
              <v-icon class="material-design">open_in_new</v-icon>
            </v-list-tile-action>
            <v-list-tile-title>
              Registrarse
            </v-list-tile-title>
          </v-list-tile>
        </template>

        <template v-if="logeado">
          <v-list-tile @click="close()">
            <v-list-tile-action>
              <v-icon class="material-design">cancel</v-icon>
            </v-list-tile-action>
            <v-list-tile-title>
              Cerrar sesion
            </v-list-tile-title>
          </v-list-tile>
        </template>
        
       
        
      </v-list>
    </v-navigation-drawer>
    <v-toolbar
      color="secondary"
      dark
      app
      :clipped-left="$vuetify.breakpoint.mdAndUp"
      fixed
    >
      <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
        <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
        <span class="hidden-sm-and-down">Factura</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon>
        <v-icon>apps</v-icon>
      </v-btn>
    </v-toolbar>
    <v-content>
      <v-container fluid fill-height>
        <v-slide-y-transition mode="out-in">
          <router-view/>
        </v-slide-y-transition>
      </v-container>
    </v-content>
    <v-footer dark height="auto">
      <v-layout justify-center>
        <v-flex text-xs-center>
          <v-card flat tile color="primary" class="white--text">
            <v-card-text class="white--text pt-0">
             DHRA &copy;2019
            </v-card-text>
          </v-card>
        </v-flex>
      </v-layout>
    </v-footer>

  </v-app>
</template>


<script>

export default {
  name: 'App',
  data () {
    return {
      drawer: true,
      logeado: localStorage.getItem('logeado')
     }
  },
  methods: {
    close() {
        localStorage.removeItem('email')
        localStorage.removeItem('contraseña')
        localStorage.removeItem('logeado')
        localStorage.removeItem('id')
        window.parent.location.reload()
        window.top.location.href = "/"
    }
  }
}
</script>
