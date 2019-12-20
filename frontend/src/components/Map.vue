<template>
  <div class="App"></div>
</template>

<script>
// import MarkerClusterer from '@google/markerclusterer';

import gmapsInit from "./utils/gmaps";
import axios from "axios";

import { mount } from "@vue/test-utils";

export default {
  name: `Map`,
  props: {
    events: {
      type: Array,
      default: []
    }
  },
  data() {
    return {
      locations: [],
      map: {},
      google: {},
      data: this.$root.$data
    };
  },
  watch: {
    events: function() {
      console.log(this.events);

      this.getLocation();
    }
  },
  async mounted() {
    try {
      this.google = await gmapsInit();
      const geocoder = new google.maps.Geocoder();
      this.map = new google.maps.Map(this.$el);

      if (navigator.geolocation) {
        console.log("you can use me");
        navigator.geolocation.getCurrentPosition(this.displayLocationInfo);
      } else {
        console.log("Ups");
      }
      console.log(this.locations);
    } catch (error) {
      // eslint-disable-next-line no-console
      console.error(error);
    }
  },
  methods: {
    getLocation: function() {
      console.log(this.events);
      this.events.map(event => {
        var location =
          event.place.street +
          " " +
          event.place.number +
          ", " +
          event.place.city;
        axios
          .get("https://maps.googleapis.com/maps/api/geocode/json", {
            params: {
              address: location,
              key: this.data.key
            }
          })
          .then(response => {
            if (response.data.results[0]) {
              var location = {
                position: {
                  lat: response.data.results[0].geometry.location.lat,
                  lng: response.data.results[0].geometry.location.lng
                }
              };
              const map = this.map;
              const marker = new google.maps.Marker({ ...location, map });
              marker.addListener(`click`, () =>
                this.markerClickHandler(marker, event)
              );
            }
          })
          .catch(function(error) {
            console.log(error);
          });
      });
    },
    markerClickHandler(marker, event) {
      this.map.setZoom(15);
      this.map.setCenter(marker.getPosition());
      window.open('http://localhost:8080/event/'+event.id) //your frontend api
      console.log(event);
    },
    displayLocationInfo(position) {
      const lng = position.coords.longitude;
      const lat = position.coords.latitude;

      var latlng = new this.google.maps.LatLng(lat, lng);
      var geocoder = new this.google.maps.Geocoder();
      geocoder.geocode({ latLng: latlng }, (results, status) => {
        if (status == this.google.maps.GeocoderStatus.OK) {
          if (results[1]) {
            this.map.setCenter(results[0].geometry.location);
            this.map.fitBounds(results[0].geometry.viewport);
            this.map.setZoom(11);
          }
        }
      });
      // this.map.fitBounds(results[0].geometry.viewport);
      console.log(`longitude: ${lng} | latitude: ${lat}`);
    }
  }
};
</script>

<style>
html,
body {
  margin: 0;
  padding: 0;
}

.App {
  width: 100%;
  height: 65vh;
}
</style>
