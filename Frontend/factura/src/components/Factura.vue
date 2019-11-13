<template>
<v-layout align-start>
    <v-flex>
        <v-toolbar flat color="white">
            <v-toolbar-title>Factura</v-toolbar-title>
            <v-divider class="mx-2" inset vertical></v-divider>
            <v-spacer></v-spacer>

            
            <v-dialog v-model="verCostos" max-width="1000px">
                <v-card>
                    <v-card-title>
                        <span class="headline">Agregar costo</span>
                    </v-card-title>
                    <v-card-text>
                        <v-container grid-list-md>
                            <v-layout wrap>
                                <v-flex xs12 sm4 md4 lg4 xl4>
                                    <v-select v-model="motivo" :items="motivos" label="Motivo">
                                    </v-select>
                                </v-flex>
                                <v-flex xs12 sm4 md4 lg4 xl4>
                                    <v-select v-model="isEfectivo" :items="expresadoEn" label="Expresado en">
                                    </v-select>
                                </v-flex>
                                <v-flex xs12 sm4 md4 lg4 xl4>
                                    <v-select v-model="isInit" :items="tiempo" label="Final o Inicial">
                                    </v-select>
                                </v-flex>
                                <v-flex xs12 sm4 md4 lg4 xl4 v-if="isEfectivo">
                                    <v-text-field prefix="$" v-model="valor" label="Valor">
                                    </v-text-field>
                                </v-flex>
                                <v-flex xs12 sm4 md4 lg4 xl4 v-if="!isEfectivo">
                                    <v-text-field suffix="%" v-model="valor" label="Valor">
                                    </v-text-field>
                                </v-flex>
                            </v-layout>
                        </v-container>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn @click="ocultarMedicamentos()" color="blue darken" flat>
                            Cancelar
                        </v-btn>
                        <v-btn @click="agregarCosto(),limpiarCosto()" color="success" flat>
                            Agregar
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-toolbar>
        

        <v-container grid-list-sm class="pa-4 white" v-if="verNuevo">
            <h3>Datos de la Factura</h3>
            <v-layout row wrap>
                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-menu v-model="modal1" :close-on-content-click="false" :nudge-right="10" lazy transition="scale-transition"
                        offset-y min-width="290px" >
                        <template v-slot:activator="{ on }">
                            <v-text-field v-model="fechaEmision" label="Fecha de Emision" prepend-icon="event" readonly v-on="on"></v-text-field>
                        </template>
                        <v-date-picker v-model="fechaEmision" @input="modal1 = false" ></v-date-picker>
                    </v-menu>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-menu v-model="modal2" :close-on-content-click="false" :nudge-right="10" lazy transition="scale-transition"
                        offset-y min-width="290px" >
                        <template v-slot:activator="{ on }">
                            <v-text-field v-model="fechaPago" label="Fecha de Pago" prepend-icon="event" readonly v-on="on"></v-text-field>
                        </template>
                        <v-date-picker v-model="fechaPago" @input="modal2 = false" ></v-date-picker>
                    </v-menu>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-text-field value="10.00" prefix="$" type="number" v-model="totalFacturado" label="Total Facturado">
                    </v-text-field>
                </v-flex>
            </v-layout>
        </v-container>

        <v-container grid-list-sm class="pa-4 white" v-if="verNuevo">
            <h3>Tasa y Plazo</h3>
            <v-layout row wrap>
                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-select v-model="diasPorAño" :items="diasPorAñoarr" label="Dias por año">
                    </v-select>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-select v-model="plazoDeTasa" :items="plazos" label="Plazo de la Tasa">
                    </v-select>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-text-field value="plazoDeTasa" v-model="plazoDeTasa" label="Plazo de la Tasa (Dias)">
                    </v-text-field>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-select v-model="isNominal" :items="typeTasa" label="Tipo de Tasa">
                    </v-select>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4 v-if="!isNominal">
                    <v-text-field suffix="%" v-model="tasaEfectiva" label="Tasa Efectiva">
                    </v-text-field>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4 v-if="isNominal">
                    <v-text-field suffix="%" v-model="tasaNominal" label="Tasa Nominal">
                    </v-text-field>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4 v-if="isNominal">
                    <v-select v-model="periodoCapital" :items="plazos" label="Periodo de Capitalizacion">
                    </v-select>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4 v-if="isNominal">
                    <v-text-field value="periodoCapital" v-model="periodoCapital" label="Periodo de Capitalizacion (Dias)">
                    </v-text-field>
                </v-flex>

                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-menu v-model="modal3" :close-on-content-click="false" :nudge-right="10" lazy transition="scale-transition"
                        offset-y min-width="290px" >
                        <template v-slot:activator="{ on }">
                            <v-text-field v-model="fechaDescuento " label="Fecha de Descuento" prepend-icon="event" readonly v-on="on"></v-text-field>
                        </template>
                        <v-date-picker v-model="fechaDescuento" @input="modal3 = false" ></v-date-picker>
                    </v-menu>
                </v-flex>
            </v-layout>
        </v-container>
        <v-container grid-list-sm class="pa-4 white" v-if="verNuevo">
            <h3>Costos/Gastos</h3>
            <v-layout row wrap>
                <v-flex xs12 sm2 md2 lg2 xl2>
                    <v-btn class="mx-2" @click="mostrarMedicamentos()" small fab dark color="teal">
                        <v-icon dark>add</v-icon>
                    </v-btn>
                </v-flex>

                <v-flex xs12 sm2 md2 lg2 xl2 v-if="errorMedicamento">
                    <div class="red--text" v-text="errorMedicamento">
                    </div>
                </v-flex>

                <v-flex xs12 sm12 md12 lg12 xl12>
                    <v-data-table :headers="cabeceraCostos" :items="auxCostos" hide-actions class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td class="justify-center layout px-0">
                                <v-icon small class="mr-2" @click="eliminarCosto(auxCostos,props.item)">
                                    delete
                                </v-icon>
                            </td>
                            <td>{{props.item.motivo}}</td>
                            <td v-if="props.item.isEfectivo">${{props.item.valor}}</td>
                            <td v-if="!props.item.isEfectivo">{{props.item.valor * 100}}%</td>
                            <td v-if="props.item.isInit" >Inicial</td>
                            <td v-if="!props.item.isInit" >Final</td>
                            <td v-if="props.item.isEfectivo" >Efectivo</td>
                            <td v-if="!props.item.isEfectivo" >Porcentaje</td>
                        </template>
                        <template slot="no-data">
                            <h3>No hay costos agregados.</h3>
                        </template>
                    </v-data-table>
                </v-flex>

                <v-flex xs12 sm12 md12 lg12 xl12>
                    <v-btn @click="limpiar()" color="blue darken-1" flat>Limpiar</v-btn>
                    <v-btn @click="guardar(),obtenerResultado()" color="success">Calcular Factura</v-btn>
                </v-flex>
            </v-layout>
        </v-container>


        
        <v-flex xs12 sm12 md12 lg12 xl12 v-if="result">
            <h2>Resultados:</h2>
                    <v-data-table :headers="cabeceraResultados" :items="resultados" hide-actions class="elevation-1">
                        <template slot="items" slot-scope="props">
                            <td>{{props.item.tea*100}}%</td>
                            <td>${{props.item.valorNeto}}</td>
                            <td>${{props.item.valorRecibido}}</td>
                            <td>${{props.item.valorEntregado}}</td>
                            <td>{{props.item.tcea*100}}%</td>
                        </template>
                    </v-data-table>
            </v-flex>
    </v-flex>
</v-layout>
</template>

<script>
import axios from 'axios'

export default {
    data() {
        return {
            modal1: false,
            modal2: false,
            modal3: false,
            fechaConsulta: new Date(),
            fechaEmision: '',
            fechaPago: '',
            totalFacturado: '',
            diasPorAño: '',
            diasPorAñoarr: [360, 350],
            plazoDeTasa:'',
            plazos: [{
                    text: 'Diario',
                    value: 1
                },
                {
                    text: 'Quincenal',
                    value: 15
                },
                {
                    text: 'Mensual',
                    value: 30
                },
                {
                    text: 'Bimestral',
                    value: 60
                },
                {
                    text: 'Trimestral',
                    value: 90
                },
                {
                    text: 'Cuatrimestral',
                    value: 120
                },
                {
                    text: 'Semestral',
                    value: 180
                },
                {
                    text: 'Anual',
                    value: 360
                },
                {
                    text: 'Especial',
                    value: 22
                }],
            
            isNominal: false,
            typeTasa: [{
                text: 'Nominal',
                value: true
            },
            {
                text: 'Efectiva',
                value: false  
            }],
            tasaEfectiva: '',
            tasaNominal: '',
            periodoCapital: 0,
            fechaDescuento: '',
            costos: [],
            auxCostos: [],
            usuarioId: 1,
            facturaId: 1,
            motivo: '',
            valor: '',
            isEfectivo: true,
            isInit: true,

            motivos: [{
                text: 'Portes',
                value: 'Portes'
            },
            {
                text: 'Fotocopias',
                value: 'Fotocopias'
            },
            {
                text: 'Seguro',
                value: 'Seguro'
            },
            {
                text: 'Otros',
                value: 'Otros'
            }],

            expresadoEn: [{
                text: 'Efectivo',
                value: true
            },
            {
                text: 'Porcentaje',
                value: false
            }],

            tiempo: [{
                text: 'Inicial',
                value: true
            },
            {
                text: 'Final',
                value: false
            }],

            dialog: false,

            cabeceraCostos: [{
                    text: 'Borrar',
                    value: 'borrar',
                    sortable: false
                },
                {
                    text: 'Motivo',
                    value: 'motivo',
                    sortable: false
                },
                {
                    text: 'Valor',
                    value: 'valor',
                    sortable: false
                },
                {
                    text: 'Es Inicial',
                    value: 'isInit',
                    sortable: true
                },
                {
                    text: 'Es Efectivo',
                    value: 'isEfectivo',
                    sortable: false
                }
            ],
            cabeceraResultados: [{
                    text: 'TEA',
                    sortable: false
                },
                {
                    text: 'Valor Neto',
                    sortable: false
                },
                {
                    text: 'Valor Recibido',
                    sortable: false
                },
                {
                    text: 'Valor Entregado',
                    sortable: false
                },
                {
                    text: 'TCEA',
                    sortable: false
                }
            ],
            resultados: [],
            result: false,
            verNuevo: 1,
            verCostos: 0,
            errorMedicamento: '',
            texto: '',
        }
    },

    watch: {
        dialog(val) {
            val || this.close()
        }
    },

    created() {
        this.listar();
        this.listarPacientes();
        this.listarMedicamento();
    },

    methods: {

        mostrarNuevo() {
            this.verNuevo = 1;
        },

        mostrarMedicamentos() {
            this.verCostos = 1;
        },
        ocultarMedicamentos() {
            this.verCostos = 0;
        },

        listar() {
            let me = this;
            axios.get('api/Factura').then(function (response) {
                //console.log(response);
                me.ordenes = response.data;
            }).catch(function (error) {
                console.log(error);
            });
        },

        obtenerResultado(){
            let me = this;
            me.resultados = [],
            axios.get('api/Factura/last').then(function (response) {
                //console.log(response);
                //me.resultados = response.data;
                me.resultados.push(response.data)
            }).catch(function (error) {
                console.log(error);
            });
        },

        agregarCosto() {
            let me = this;
            this.errorMedicamento = null;
            if (!me.isEfectivo) {
                me.valor = me.valor / 100
            }
            this.auxCostos.push({
                facturaId: 1,
                motivo: me.motivo,
                isEfectivo: me.isEfectivo,
                isInit: me.isInit,
                valor: me.valor
            });
            console.log(this.auxCostos);
        },

        eliminarCosto(arr, item) {
            var i = arr.indexOf(item);
            if (i !== -1) {
                arr.splice(i, 1);
            }
        },

        guardar() {
            let me = this;
            me.costos = me.auxCostos;
            console.log(me.fechaEmision);
            axios
                .post("api/Factura", {
                    fechaConsulta: new Date(),
                    fechaEmision: me.fechaEmision,
                    fechaPago: me.fechaPago,
                    totalFacturado: me.totalFacturado,
                    diasPorAño: me.diasPorAño,
                    plazoDeTasa: me.plazoDeTasa,
                    isNominal: me.isNominal,
                    tasaEfectiva: me.tasaEfectiva/100,
                    tasaNominal: me.tasaNominal/100,
                    periodoCapital: me.periodoCapital,
                    fechaDescuento: me.fechaDescuento,
                    costos: me.costos,
                    usuarioId: me.usuarioId
                })
                .then(function (response) {
                    me.result = true;
                    me.ocultarNuevo();
                    me.listar();
                    me.limpiar();
                })
                .catch(function (error) {
                    console.log(error);
                    console.log(me.detalles)
                });
        },

        limpiar() {
            this.fechaEmision = '',
            this.fechaPago = '',
            this.totalFacturado = '',
            this.diasPorAño = '',
            this.plazoDeTasa = '',
            this.tasaEfectiva = '',
            this.tasaNominal = '',
            this.fechaDescuento = '',
            this.result = false,
            this.auxCostos = [];
            this.costos = [];
        },
        limpiarCosto() {
            this.motivo = '',
            this.valor = ''
        },

    },

}
</script>