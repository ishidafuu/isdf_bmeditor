<template>
  <div>
    <button @click="addKoma()">AddKoma (N)</button>
    <button @click="removeKoma()">RemoveKoma (Del)</button>
  </div>
</template>

<script lang="ts">
import {defineComponent, onMounted, onBeforeUnmount} from 'vue'
import {useStore} from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const store = useStore()
    const addKoma = () => {
      store.commit(`${props.storeName}/addKoma`)
    }
    const removeKoma = () => {
      store.commit(`${props.storeName}/removeKoma`)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.code === 'KeyN') {
        addKoma();
      }

      if (event.code === 'Delete') {
        removeKoma();
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      addKoma,
      removeKoma,
    }
  }
})
</script>
