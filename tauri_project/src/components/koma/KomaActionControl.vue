<template>
  <div>
    <button @click="toggleAction">Toggle Action</button>
    <span>Is Action: {{ isAction }}</span>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, toRefs } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const store = useStore()
    const { storeName } = toRefs(props)

    const isAction = computed(() => store.getters[`${storeName.value}/currentKomaState`].isAction)

    const toggleAction = () => {
      store.commit(`${storeName.value}/toggleAction`)
    }

    return {
      toggleAction,
      isAction
    }
  }
})
</script>