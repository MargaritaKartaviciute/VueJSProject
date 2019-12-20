<template>
  <div class="about" id="app">
    <div class="flip-box">
      <div class="flip-box-inner">
        <div class="flip-box-front">
          <b-card
            overlay
            img-src="https://dancingastro-wpengine.netdna-ssl.com/wp-content/uploads/2017/06/ade.jpg"
            img-alt="event?"
            text-variant="white"
            title="Want to keep up with all parties?"
          >
            <b-card-text>Here you can find the nearest event. Create your profile and make your events list. All events in one place.</b-card-text>
          </b-card>
        </div>
        <div class="flip-box-back">
          <b-card
            overlay
            img-src="http://somethingdifferentcompanies.com/wp-content/uploads/2016/06/event-image.jpeg"
            img-alt=""
            text-variant="white"
            title="And never miss any event?"
          >
            <b-card-text>Here you can find the nearest event. Create your profile and make your events list. All events in one place.</b-card-text>
          </b-card>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
.flip-box {
  background-color: transparent;

  border: 1px solid #f1f1f1;
  perspective: 1000px; /* Remove this if you don't want the 3D effect */
}

/* This container is needed to position the front and back side */
.flip-box-inner {
  position: relative;
  width: 100%;
  height: 100%;
  text-align: center;
  transition: transform 0.8s;
  transform-style: preserve-3d;
}

/* Do an horizontal flip when you move the mouse over the flip box container */
.flip-box:hover .flip-box-inner {
  transform: rotateY(180deg);
}

/* Position the front and back side */
.flip-box-front,
.flip-box-back {
  position: absolute;
  width: 100%;
  height: 100%;
  backface-visibility: hidden;
}

/* Style the front side */
.flip-box-front {
  background-color: #bbb;
  color: black;
}

/* Style the back side */
.flip-box-back {
  background-color: dodgerblue;
  color: white;
  transform: rotateY(180deg);
}
#app {
  height: 110vh;
  text-align: center;
}
@media only screen and (min-width: 768px) {
  #app {
    height: 100vh;
    text-align: center;
  }
}
@media only screen and (min-width: 1000px) {
  #app {
    height: 70vh;
    text-align: center;
  }
}
</style>
