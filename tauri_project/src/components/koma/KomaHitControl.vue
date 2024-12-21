<template>
  <div>
    <div>
      <button @click="changeHitX(-1)">Hit X -</button>
      <span>Hit X: {{ hitX }}</span>
      <button @click="changeHitX(1)">Hit X +</button>
    </div>
    <div>
      <button @click="changeHitY(-1)">Hit Y -</button>
      <span>Hit Y: {{ hitY }}</span>
      <button @click="changeHitY(1)">Hit Y +</button>
    </div>
    <div>
      <button @click="changeHitW(-1)">Hit Width -</button>
      <span>Hit Width: {{ hitW }}</span>
      <button @click="changeHitW(1)">Hit Width +</button>
    </div>
    <div>
      <button @click="changeHitH(-1)">Hit Height -</button>
      <span>Hit Height: {{ hitH }}</span>
      <button @click="changeHitH(1)">Hit Height +</button>
    </div>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed} from 'vue'
import {useStore} from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const store = useStore()

    const hitX = computed(() => store.getters[`${props.storeName}/currentKomaState`].attack.hitX)
    const hitY = computed(() => store.getters[`${props.storeName}/currentKomaState`].attack.hitY)
    const hitW = computed(() => store.getters[`${props.storeName}/currentKomaState`].attack.hitW)
    const hitH = computed(() => store.getters[`${props.storeName}/currentKomaState`].attack.hitH)

    const changeHitX = (amount: number) => {
      store.commit(`${props.storeName}/changeHitX`, amount)
    }

    const changeHitY = (amount: number) => {
      store.commit(`${props.storeName}/changeHitY`, amount)
    }

    const changeHitW = (amount: number) => {
      store.commit(`${props.storeName}/changeHitW`, amount)
    }

    const changeHitH = (amount: number) => {
      store.commit(`${props.storeName}/changeHitH`, amount)
    }

    return {
      hitX,
      hitY,
      hitW,
      hitH,
      changeHitX,
      changeHitY,
      changeHitW,
      changeHitH,
    }
  }
})
</script>