<template>
  <div id="app">
    <b-alert v-model="showDismissibleAlert" variant="danger" dismissible>Something went wrong</b-alert>
    <b-alert v-model="showDismissibleAlert2" variant="success" dismissible>You created new event</b-alert>
    <b-form @submit="onSubmit" @reset="onReset">
      <b-card bg-variant="dark">
        <b-form-group
          text-variant="primary"
          label-cols-md="3"
          label="Address"
          label-size="lg"
          label-class="font-weight-bold pt-0 text-white"
          class="mb-0"
        >
          <b-form-group
            label-cols-sm="3"
            label="Street:"
            label-align-sm="right"
            label-for="nested-street"
            label-class="text-white"
          >
            <b-form-input id="nested-street" v-model="street"></b-form-input>
          </b-form-group>

          <b-form-group
            label-cols-sm="3"
            label="Number:"
            label-align-sm="right"
            label-for="nested-city"
            label-class="text-white"
          >
            <b-form-input id="nested-city" type="number" v-model="number"></b-form-input>
          </b-form-group>

          <b-form-group
            label-cols-md="3"
            label="City:"
            label-align-sm="right"
            label-for="nested-country"
            label-class="text-white"
          >
            <b-form-input id="nested-city" v-model="city"></b-form-input>
          </b-form-group>
        </b-form-group>
        <b-form-group
          label-cols-md="3"
          label="Name"
          label-size="lg"
          label-class="font-weight-bold pt-0 text-white"
          class="mb-0"
        >
          <b-form-group label-cols-sm="3" label-align-sm="right" label-for="nested-name">
            <b-form-input id="nested-name" v-model="name"></b-form-input>
          </b-form-group>
        </b-form-group>
        <b-form-group
          label-cols-md="3"
          label="Price"
          label-size="lg"
          label-class="font-weight-bold pt-0 text-white"
          class="mb-0"
        >
          <b-form-group label-cols-sm="3" label-align-sm="right" label-for="nested-name">
            <b-form-input id="nested-price" type="number" v-model="price"></b-form-input>
          </b-form-group>
        </b-form-group>
        <b-form-group
          label-cols-md="3"
          label="Date"
          label-size="lg"
          label-class="font-weight-bold pt-0 text-white"
          class="mb-0"
        >
          <b-form-group label-cols-sm="3" label-align-sm="right" label-for="nested-name">
            <b-form-input id="nested-date" type="date" v-model="date"></b-form-input>
          </b-form-group>
        </b-form-group>
        <b-form-group
          label-cols-md="3"
          label="Description"
          label-size="lg"
          label-class="font-weight-bold pt-0 text-white"
          class="mb-0"
        >
          <b-form-group label-cols-sm="3" label-align-sm="right" label-for="nested-name">
            <b-form-textarea
              id="nested-description"
              placeholder="Enter something..."
              v-model="description"
              rows="1"
              max-rows="1"
              maxlength="80"
            ></b-form-textarea>
          </b-form-group>
        </b-form-group>
        <b-form-group
          label-cols-lg="3"
          label="Image"
          label-size="lg"
          label-class="font-weight-bold pt-0 text-white"
          class="mb-0"
        >
          <b-form-file
            label-cols-sm="3"
            v-model="file"
            placeholder="Choose a file or drop it here..."
            drop-placeholder="Drop file here..."
          ></b-form-file>
          <div class="mt-3 text-white">Selected file: {{ file ? file.name : '' }}</div>
        </b-form-group>
        <b-row>
          <b-col lg="8"></b-col>
          <b-col lg="2" class="pb-2">
            <b-button type="submit" block variant="outline-success">Submit</b-button>
          </b-col>
          <b-col lg="2" class="pb-2">
            <b-button type="reset" block variant="outline-danger">Reset</b-button>
          </b-col>
        </b-row>
      </b-card>
    </b-form>
  </div>
</template>
<script>
import axios from "axios";

import { authenticationService } from "../_services/authentication.service";
import { userService } from "../_services/user.service";

import "sweetalert2/dist/sweetalert2.min.css";
export default {
  data() {
    return {
      user: authenticationService.currentUserValue,
      users: [],
      event: {},
      file: null,
      street: "",
      number: 0,
      city: "",
      name: "",
      price: 0,
      date: "",
      description: "",
      image: "",
      previewImage: null,
      api: this.$root.$data,
      showDismissibleAlert: false,
      showDismissibleAlert2: false
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      const reader = new FileReader();
      if (this.file != null) {
      console.log("I'm here");
      // console.log(this.file);
      reader.readAsDataURL(this.file);
      reader.onload = e => {
        this.previewImage = e.target.result;
        // console.log(this.previewImage);
      };
      }
      // console.log(this.previewImage);

      this.showAlert();
    },
    onReset(evt) {
      evt.preventDefault();
      // Reset our form values
      this.street = "";
      this.name = "";
      this.number = 0;
      this.city = "";
      this.price = 0;
      this.date = "";
      this.image = "";
      this.description = "";
    },
    showAlert() {
      // Use sweetalert2
      this.$swal
        .fire({
          title: "Are you sure?",
          text: "You won't be able to revert this!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          confirmButtonText: "Yes, create it!"
        })
        .then(result => {
          if (result.value) {
            axios
              .post(this.api.value + "/events", {
                name: this.name,
                date: this.date,
                description: this.description,
                price: this.price,
                image: this.previewImage,
                place: {
                  city: this.city,
                  number: this.number,
                  street: this.street
                }
              })
              .then(
                response => (
                  console.log(response.data),
                  (this.showDismissibleAlert2 = true),
                  this.$swal.fire(
                    "Created!",
                    "Your file has been created.",
                    "success"
                  )
                )
              )
              .catch(err => {
                this.showDismissibleAlert = true;
                console.log(err);
              });
          }
        });
      // this.$swal("Hello Vue world!!!");
    }
  }
};
</script>
<style lang="scss" scoped>
#app {
  height: 110vh;
}
@media only screen and (min-width: 768px) {
  #app {
    height: 100vh;
  }
}
@media only screen and (min-width: 1000px) {
  #app {
    height: 70vh;
  }
}
</style>